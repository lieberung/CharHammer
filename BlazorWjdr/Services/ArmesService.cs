namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArmesService
    {
        private readonly CompetencesEtTalentsService _competencesEtTalentsService;
        
        private Dictionary<int, ArmeAttributDto>? _cacheArmeAttribut;
        private Dictionary<int, ArmeDto>? _cacheArme;
        private List<ArmeDto>? _allArmes;

        public ArmesService(CompetencesEtTalentsService competencesEtTalentsService)
        {
            _competencesEtTalentsService = competencesEtTalentsService;
        }

        protected Dictionary<int, ArmeAttributDto> AllAttributsDArme
        {
            get
            {
                if (_cacheArmeAttribut == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null Arme return.
                return _cacheArmeAttribut;
#pragma warning restore CS8603 // Possible null Arme return.
            }
        }

        public List<ArmeAttributDto> AllGroupesDArmes => AllAttributsDArme.Values.Where(a => a.Type == "groupe").OrderBy(g => g.Nom).ToList();

        protected List<ArmeDto> AllArmes
        {
            get
            {
                if (_allArmes == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null Arme return.
                return _allArmes;
#pragma warning restore CS8603 // Possible null Arme return.
            }
        }
        
        public Task<ArmeDto[]> Items()
        {
            return Task.FromResult(AllArmes.ToArray());
        }

        public ArmeAttributDto GetAttributDArme(int id)
        {
            if (_cacheArmeAttribut == null)
                Initialize();
#pragma warning disable CS8602 // DeArme of a possibly null Arme.
            return _cacheArmeAttribut[id];
#pragma warning restore CS8602 // DeArme of a possibly null Arme.
        }

        public IEnumerable<ArmeDto> GetArmes(IEnumerable<int> ids) => ids.Select(GetArme).ToArray();
        public ArmeDto GetArme(int id)
        {
            if (_cacheArme == null)
                Initialize();
#pragma warning disable CS8602 // DeArme of a possibly null Arme.
            return _cacheArme[id];
#pragma warning restore CS8602 // DeArme of a possibly null Arme.
        }

        private void Initialize()
        {
            _cacheArmeAttribut = DataSource.JsonLoader
                .GetRootArmeAttribut()
                .items
                .Select(t => new ArmeAttributDto
                {
                    Id = t.id,
                    Type = t.type,
                    Nom = t.nom,
                    Description = t.description
                }).ToDictionary(k => k.Id, v => v);
            
            _cacheArme = DataSource.JsonLoader
                .GetRootArme()
                .items
                .Select(l => new ArmeDto
                {
                    Id = l.id,
                    Nom = l.nom,
                    Description = l.description ?? "",
                    Attributs = l.attributs.Select(id => _cacheArmeAttribut[id]).ToList(),
                    Degats = l.degats,
                    Disponibilite = l.dispo,
                    Encombrement = l.enc,
                    Groupes = l.groupes.Select(id => _cacheArmeAttribut[id]).ToList(),
                    Portee = l.portee,
                    Prix = l.prix,
                    Rechargement = l.rechargement,
                    TalentsDeMaitrise = l.talents.Select(id => _competencesEtTalentsService.GetTalent(id)).ToList()
                })
                .ToDictionary(k => k.Id, v => v);
            
            _allArmes = _cacheArme.Values.OrderBy(a => a.Nom).ToList();
        }
    }
}
