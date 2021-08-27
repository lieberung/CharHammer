using System.Diagnostics;

namespace BlazorWjdr.Services
{
    using BlazorWjdr.DataSource.JsonDto;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RacesService
    {
        private readonly LieuxService _lieuxService;
        private readonly ProfilsService _profilsService;

        private readonly List<JsonRace> _dataRaces;
        private Dictionary<int, RaceDto>? _cacheRace;

        public RacesService(List<JsonRace> dataRaces, LieuxService lieuxService, ProfilsService profilsService)
        {
            _dataRaces = dataRaces;
            _lieuxService = lieuxService;
            _profilsService = profilsService;
        }

        public List<RaceDto> AllRaces
        {
            get
            {
                if (_cacheRace == null)
                    Initialize();
                Debug.Assert(_cacheRace != null, nameof(_cacheRace) + " != null");
                return _cacheRace.Values.ToList();
            }
        }

        public RaceDto Elfes => GetRace(25);
        public RaceDto Humains => GetRace(1);
        public RaceDto HumainsMutants => GetRace(64);
        public RaceDto Halfelings => GetRace(26);
        public RaceDto Nains => GetRace(27);
        public RaceDto Gnomes => GetRace(63);
        
        public RaceDto GetRace(int id)
        {
            if (_cacheRace == null)
                Initialize();
            Debug.Assert(_cacheRace != null, nameof(_cacheRace) + " != null");
            return _cacheRace[id];
        }

        private void Initialize()
        {
            _cacheRace = _dataRaces
                .Select(r => new RaceDto
                {
                    Id = r.id,
                    Description = r.description,
                    Lieux = (r.lieux_ids ?? System.Array.Empty<int>()).Select(id => _lieuxService.GetLieu(id)).ToArray(),
                    Profil = r.profil_id.HasValue ? _profilsService.GetProfil(r.profil_id.Value) : null,
                    GroupOnly = r.group_only,
                    NomFeminin = r.nom_feminin,
                    NomMasculin = r.nom_masculin,
                    ParentId = r.parent_id,
                    PourPj = r.pj
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var race in _cacheRace.Values.Where(d => d.ParentId.HasValue))
            {
                race.Parent = _cacheRace[race.ParentId!.Value];
            }
            
            foreach (var lieu in _cacheRace.Values)
            {
                lieu.SousElements.AddRange(_cacheRace.Values
                    .Where(c=>c.Parent == lieu)
                    .OrderBy(c => c.NomMasculin));                
            }

            //_dataRaces.Clear();
        }
    }
}
