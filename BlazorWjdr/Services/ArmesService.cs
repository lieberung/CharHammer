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
        private List<ArmeDto>? _allArmex;

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
        
        protected List<ArmeDto> AllArmes
        {
            get
            {
                if (_allArmex == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null Arme return.
                return _allArmex;
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
                    Disponibilite = l.disponibilite,
                    Encombrement = l.enc,
                    Groupe = l.groupe,
                    Portee = l.portee,
                    Prix = l.prix,
                    Rechargement = l.rechargement,
                    TalentsDeMaitrise = l.talents.Select(id => _competencesEtTalentsService.GetTalent(id)).ToList()
                })
                .ToDictionary(k => k.Id, v => v);
            
            _allArmex = _cacheArme.Values.ToList();
        }
    }
}
