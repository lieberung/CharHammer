namespace BlazorWjdr.Services
{
    using BlazorWjdr.DomainModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarrieresService
    {
        private List<CarriereDto>? _allCarrieres = null;
        private Dictionary<int, CarriereDto>? _cacheCarrieres = null;

        private ProfilsService _profilsService;
        private CompetencesEtTalentsService _competencesEtTalentsService;
        private ChoixCompetencesEtTalentsService _choixCompetencesEtTalentsService;

        public CarrieresService(
            ProfilsService profilsService,
            CompetencesEtTalentsService competencesEtTalentsService,
            ChoixCompetencesEtTalentsService choixCompetencesEtTalentsService)
        {
            _profilsService = profilsService;
            _competencesEtTalentsService = competencesEtTalentsService;
            _choixCompetencesEtTalentsService = choixCompetencesEtTalentsService;
        }

        protected List<CarriereDto> AllCarrieres
        {
            get
            {
                if (_allCarrieres == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null reference return.
                return _allCarrieres;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public CarriereDto[] GetCarrieres(int[] ids) => ids.Select(id => GetCarriere(id)).ToArray();
        public CarriereDto GetCarriere(int id)
        {
            if (_cacheCarrieres == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheCarrieres[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public Task<CarriereDto[]> GetCarrieresProposant(CompetenceDto competence)
            => Task.FromResult(AllCarrieres
                .Where(c => c.Competences.Contains(competence)) // ToDo: choix
                .ToArray());
        public Task<CarriereDto[]> GetCarrieresProposant(TalentDto talent)
            => Task.FromResult(AllCarrieres
                .Where(c => c.Talents.Contains(talent)) // ToDo: choix
                .ToArray());

        public Task<CarriereDto[]> ItemsCarrieres()
        {
            return Task.FromResult(AllCarrieres.ToArray());
        }

        private void Initialize()
        {
            _allCarrieres = DataSource.JsonLoader
                .GetRootCarriere()
                .items
                .Select(c => new CarriereDto
                {
                    Id = c.id,
                    Libelle = c.libelle,
                    Description = c.description ?? "",
                    CarriereMereId = c.fk_parentcarriereid,
                    ChoixCompetencesIds = (c.fk_choixcompetences ?? new int[0]).ToList(),
                    ChoixTalentsIds = (c.fk_choixtalents ?? new int[0]).ToList(),
                    Complete = c.complete,
                    DebouchesIds = c.fk_debouches ?? new int[0],
                    Dotations = c.dotations,
                    EstUneCarriereAvancee = c.avancee,
                    Image = c.image,
                    PlanDeCarriere = _profilsService.GetProfil(c.fk_plandecarriereid),
                    Restriction = c.restriction ?? "",
                    Source = c.source ?? "",
                    SourceId = c.fk_sourceid,
#pragma warning disable CS8604 // Possible null reference argument.
                    Competences = (c.fk_competences ?? Array.Empty<int>()).Any() ?
                        _competencesEtTalentsService.GetCompetences(c.fk_competences).ToList()
                        : new List<CompetenceDto>(),
                    Talents = (c.fk_talents ?? Array.Empty<int>()).Any() ?
                        _competencesEtTalentsService.GetTalents(c.fk_talents).ToList()
                        : new List<TalentDto>(),
                    ChoixCompetences = (c.fk_choixcompetences ?? Array.Empty<int>()).Any() ? 
                        _choixCompetencesEtTalentsService.GetChoixCompetences(c.fk_choixcompetences).ToList()
                        : new List<CompetenceDto[]>(),
                    ChoixTalents = (c.fk_choixtalents ?? Array.Empty<int>()).Any() ?
                        _choixCompetencesEtTalentsService.GetChoixTalents(c.fk_choixtalents).ToList()
                        : new List<TalentDto[]>()
#pragma warning restore CS8604 // Possible null reference argument.
                })
                .OrderBy(t => t.Libelle)
                .ToList();

            _cacheCarrieres = _allCarrieres.ToDictionary(k => k.Id, v => v);

            foreach (var carriere in _allCarrieres.Where(c => c.CarriereMereId.HasValue))
#pragma warning disable CS8629 // Nullable value type may be null.
                carriere.CarriereMere = _cacheCarrieres[carriere.CarriereMereId.Value];
#pragma warning restore CS8629 // Nullable value type may be null.

            foreach (var carriere in _allCarrieres.Where(c => c.DebouchesIds.Any()))
                carriere.Debouches = GetCarrieres(carriere.DebouchesIds).ToList();

            foreach (var carriere in _allCarrieres)
                carriere.Filieres = _allCarrieres.Where(c => c.Debouches.Contains(carriere)).ToList();
        }
    }
}
