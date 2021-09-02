namespace BlazorWjdr.Services
{
    using System;
    using System.Diagnostics;
    using DataSource.JsonDto;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ReglesService
    {
        private readonly CarrieresService _carrieresService;
        private readonly CompTalentsEtTraitsService _compTalentsEtTraitsService;
        private readonly BestiolesService _bestiolesService;
        private readonly TablesService _tablesService;
        private readonly LieuxService _lieuxService;

        private readonly List<JsonRegle> _dataRegles;
        private Dictionary<int, RegleDto>? _cacheRegle;

        public ReglesService(List<JsonRegle> dataRegles, CarrieresService carrieresService,
            CompTalentsEtTraitsService compTalentsEtTraitsService,
            BestiolesService bestiolesService, 
            TablesService tablesService, 
            LieuxService lieuxService)
        {
            _dataRegles = dataRegles;
            _carrieresService = carrieresService;
            _compTalentsEtTraitsService = compTalentsEtTraitsService;
            _bestiolesService = bestiolesService;
            _tablesService = tablesService;
            _lieuxService = lieuxService;
        }

        public List<RegleDto> AllRegles
        {
            get
            {
                if (_cacheRegle == null)
                    Initialize();
                Debug.Assert(_cacheRegle != null, nameof(_cacheRegle) + " != null");
                return _cacheRegle.Values.ToList();
            }
        }

        public RegleDto GetRegle(int id)
        {
            if (_cacheRegle == null)
                Initialize();
            Debug.Assert(_cacheRegle != null, nameof(_cacheRegle) + " != null");
            return _cacheRegle[id];
        }

        private void Initialize()
        {
            _cacheRegle = _dataRegles
                .Select(r => new RegleDto
                {
                    Id = r.id,
                    Html = r.html,
                    Titre = r.titre,
                    ReglesId = r.regles ?? Array.Empty<int>(),
                    Carrieres = (r.carrieres ?? Array.Empty<int>()).Select(id => _carrieresService.GetCarriere(id)).ToArray(),
                    Competences = (r.competences ?? Array.Empty<int>()).Select(id => _compTalentsEtTraitsService.GetCompetence(id)).ToArray(),
                    ChoixCompetences = r.choixcompetences != null
                        ? r.choixcompetences.Select(choix => _compTalentsEtTraitsService.GetCompetences(choix).ToArray()).ToList()
                        : new List<CompetenceDto[]>(),
                    Talents = (r.talents ?? Array.Empty<int>()).Select(id => _compTalentsEtTraitsService.GetTalent(id)).ToArray(),
                    Traits = (r.traits ?? Array.Empty<int>()).Select(id => _compTalentsEtTraitsService.GetTrait(id)).ToArray(),
                    ChoixTalents = r.choixtalents != null
                        ? r.choixtalents.Select(choix => _compTalentsEtTraitsService.GetTalents(choix).ToArray()).ToList()
                        : new List<TalentDto[]>(),
                    Bestioles = (r.bestioles ?? Array.Empty<int>()).Select(id => _bestiolesService.GetBestiole(id)).ToArray(),
                    Tables = (r.tables ?? Array.Empty<int>()).Select(id => _tablesService.GetTable(id)).ToArray(),
                    Lieux = (r.lieux ?? Array.Empty<int>()).Select(id => _lieuxService.GetLieu(id)).ToArray(),
                    Regle = r.regle
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var regle in _cacheRegle.Values)
            {
                regle.SousRegles = regle.ReglesId.Select(id => _cacheRegle[id]).ToArray();
            }

            //_dataRegles.Clear();
        }
    }
}