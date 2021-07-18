using System.Data;

namespace BlazorWjdr.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarrieresService
    {
        private List<CarriereDto>? _allCarrieres;
        private Dictionary<int, CarriereDto>? _cacheCarrieres;

        private readonly ProfilsService _profilsService;
        private readonly CompetencesEtTalentsService _competencesEtTalentsService;
        private readonly ChoixCompetencesEtTalentsService _choixCompetencesEtTalentsService;
        private readonly ReferencesService _referencesService;

        public CarrieresService(
            ProfilsService profilsService,
            CompetencesEtTalentsService competencesEtTalentsService,
            ChoixCompetencesEtTalentsService choixCompetencesEtTalentsService,
            ReferencesService referencesService)
        {
            _profilsService = profilsService;
            _competencesEtTalentsService = competencesEtTalentsService;
            _choixCompetencesEtTalentsService = choixCompetencesEtTalentsService;
            _referencesService = referencesService;
        }

        private List<CarriereDto> AllCarrieres
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

        public IEnumerable<CarriereDto> GetCarrieres(IEnumerable<int> ids) => ids.Select(GetCarriere).ToArray();
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
        
        public Task<CarriereDto[]> GetCarrieresParuesDans(ReferenceDto reference)
            => Task.FromResult(AllCarrieres
                .Where(c => c.SourceLivre == reference)
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
                    Nom = c.libelle,
                    MotsClefDeRecherche = ConvertirCaracteres(c.libelle).ToLower().Split(' ').ToList(),
                    Description = c.description,
                    CarriereMereId = c.fk_parentcarriereid,
                    DebouchesIds = c.fk_debouches ?? Array.Empty<int>(),
                    Dotations = c.dotations,
                    EstUneCarriereAvancee = c.avancee,
                    Image = $"/images/careers/{c.id}.png",
                    PlanDeCarriere = _profilsService.GetProfil(c.fk_plandecarriereid),
                    Restriction = c.restriction ?? "",
                    Source = c.source ?? "",
                    SourceLivre = c.fk_sourceid == null ? null : _referencesService.GetReference(c.fk_sourceid.Value),
#pragma warning disable CS8604 // Possible null reference argument.
                    Competences = c.fk_competences != null ?
                        _competencesEtTalentsService.GetCompetences(c.fk_competences).ToList()
                        : new List<CompetenceDto>(),
                    Talents = c.fk_talents != null ?
                        _competencesEtTalentsService.GetTalents(c.fk_talents).ToList()
                        : new List<TalentDto>(),
                    ChoixCompetences = c.fk_choixcompetences != null ? 
                        _choixCompetencesEtTalentsService.GetChoixCompetences(c.fk_choixcompetences).ToList()
                        : new List<CompetenceDto[]>(),
                    ChoixTalents = c.fk_choixtalents  != null ?
                        _choixCompetencesEtTalentsService.GetChoixTalents(c.fk_choixtalents).ToList()
                        : new List<TalentDto[]>()
#pragma warning restore CS8604 // Possible null reference argument.
                })
                .OrderBy(t => t.Nom)
                .ToList();

            _cacheCarrieres = _allCarrieres.ToDictionary(k => k.Id, v => v);

            foreach (var carriere in _allCarrieres.Where(c => c.CarriereMereId.HasValue))
#pragma warning disable CS8629 // Nullable value type may be null.
                carriere.Parent = _cacheCarrieres[carriere.CarriereMereId.Value];
#pragma warning restore CS8629 // Nullable value type may be null.

            foreach (var carriere in _allCarrieres.Where(c => c.DebouchesIds.Any()))
                carriere.Debouches = GetCarrieres(carriere.DebouchesIds).ToList();

            foreach (var carriere in _allCarrieres)
            {
                carriere.Filieres = _allCarrieres
                    .Where(c => c.Debouches.Contains(carriere))
                    .OrderBy(c => c.Nom)
                    .ToList();
                carriere.SousElements.AddRange(_allCarrieres
                    .Where(c=> c.Parent == carriere)
                    .OrderBy(c => c.Nom));
                carriere.ScoreAcademique = CalculScoreAcademique(carriere);
                carriere.ScoreMartial = CalculScoreMartial(carriere);
            }
        }

        private int CalculScoreMartial(CarriereDto carriere)
        {
            int score = 0;
            score += carriere.Competences.Count(c => c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesAcademiques) * 2;
            score += carriere.Competences.Count(c => c.Parent == _competencesEtTalentsService.CompetenceGroupeLangue);
            score += carriere.Competences.Count(c => c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesGenerales);
            if (carriere.Competences.Any(c => c == _competencesEtTalentsService.CompetenceLireEcrire))
                score += 1;
            score += carriere.PlanDeCarriere.A * (carriere.EstUneCarriereAvancee ? 2 : 4);
            score += (carriere.PlanDeCarriere.Cc / 10) * (carriere.EstUneCarriereAvancee ? 1 : 2);
            score += (carriere.PlanDeCarriere.Ct / 10) * (carriere.EstUneCarriereAvancee ? 1 : 2);
            return score;
        }

        private int CalculScoreAcademique(CarriereDto carriere)
        {
            int score = 0;
            score += carriere.Competences.Count(c => c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesAcademiques) * 2;
            score += carriere.Competences.Count(c => c.Parent == _competencesEtTalentsService.CompetenceGroupeLangue);
            score += carriere.Competences.Count(c => c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesGenerales);
            if (carriere.Competences.Any(c => c == _competencesEtTalentsService.CompetenceLireEcrire))
                score += 1;
            score += (carriere.PlanDeCarriere.Int / 5) * (carriere.EstUneCarriereAvancee ? 1 : 2);
            return score;
        }

        #region Supprimer les caractères indésirables pour le nom du fichier

        private const string CaracteresARemplacer     = "ÀÁÂÃÄÅàáâãäåÒÓÔÕÖØòóôõöøÈÉÊËèéêëÌÍÎÏìíîïÙÚÛÜùúûüÿÑñÇç-'";
        private const string CaracteresDeRemplacement = "aaaaaaaaaaaaooooooooooooeeeeeeeeiiiiiiiiuuuuuuuuynncc  ";

        private static string ConvertirCaracteres(string chaineANettoyer)
        {
            char[] tableauFind = CaracteresDeRemplacement.ToCharArray();
            char[] tableauReplace = CaracteresARemplacer.ToCharArray();

            for (var i = 0; i < tableauReplace.Length; i++)
                chaineANettoyer = chaineANettoyer.Replace(tableauReplace[i], tableauFind[i]);

            return chaineANettoyer;
        }
        #endregion

        public CarriereDto[] Recherche(string searchText)
        {
            searchText = ConvertirCaracteres(searchText).ToLower();
            var motsClefRecherches = searchText.Split(' ').Where(c => c != "").ToList();
            
            return AllCarrieres
                .Where(c => ConvertirCaracteres(c.Nom).ToLower().Contains(searchText)
                                 || c.MotsClefDeRecherche.Intersect(motsClefRecherches).Any())
                .ToArray();
        }
    }
}
