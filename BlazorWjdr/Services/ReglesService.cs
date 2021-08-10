using System;

namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReglesService
    {
        private readonly CarrieresService _carrieresService;
        private readonly CompetencesEtTalentsService _competencesEtTalentsService;
        private readonly BestiolesService _bestiolesService;
        private readonly TablesService _tablesService;
        private readonly LieuxService _lieuxService;

        private Dictionary<int, RegleDto>? _cacheRegle;
        private List<RegleDto>? _allRegles;

        public ReglesService(CarrieresService carrieresService, CompetencesEtTalentsService competencesEtTalentsService, BestiolesService bestiolesService, TablesService tablesService, LieuxService lieuxService)
        {
            _carrieresService = carrieresService;
            _competencesEtTalentsService = competencesEtTalentsService;
            _bestiolesService = bestiolesService;
            _tablesService = tablesService;
            _lieuxService = lieuxService;
        }

        private List<RegleDto> AllRegles
        {
            get
            {
                if (_allRegles == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null Regle return.
                return _allRegles;
#pragma warning restore CS8603 // Possible null Regle return.
            }
        }
        
        public Task<RegleDto[]> Items()
        {
            return Task.FromResult(AllRegles.ToArray());
        }

        public RegleDto GetRegle(int id)
        {
            if (_cacheRegle == null)
                Initialize();
#pragma warning disable CS8602 // DeRegle of a possibly null Regle.
            return _cacheRegle[id];
#pragma warning restore CS8602 // DeRegle of a possibly null Regle.
        }

        private void Initialize()
        {
            _cacheRegle = DataSource.JsonLoader
                .GetRootRegle()
                .items
                .Select(r => new RegleDto
                {
                    Id = r.id,
                    Html = r.html,
                    Titre = r.titre,
                    ReglesId = r.regles ?? Array.Empty<int>(),
                    Carrieres = (r.carrieres ?? Array.Empty<int>()).Select(id => _carrieresService.GetCarriere(id)).ToArray(),
                    Competences = (r.competences ?? Array.Empty<int>()).Select(id => _competencesEtTalentsService.GetCompetence(id)).ToArray(),
                    ChoixCompetences = r.choixcompetences != null
                        ? r.choixcompetences.Select(choix => _competencesEtTalentsService.GetCompetences(choix).ToArray()).ToList()
                        : new List<CompetenceDto[]>(),
                    Talents = (r.talents ?? Array.Empty<int>()).Select(id => _competencesEtTalentsService.GetTalent(id)).ToArray(),
                    ChoixTalents = r.choixtalents != null
                        ? r.choixtalents.Select(choix => _competencesEtTalentsService.GetTalents(choix).ToArray()).ToList()
                        : new List<TalentDto[]>(),
                    Bestioles = (r.bestioles ?? Array.Empty<int>()).Select(id => _bestiolesService.GetBestiole(id)).ToArray(),
                    Tables = (r.tables ?? Array.Empty<int>()).Select(id => _tablesService.GetTable(id)).ToArray(),
                    Lieux = (r.lieux ?? Array.Empty<int>()).Select(id => _lieuxService.GetLieu(id)).ToArray(),
                    Regle = r.regle
                })
                .ToDictionary(k => k.Id, v => v);
            
            _allRegles = _cacheRegle.Values.ToList();

            foreach (var regle in _allRegles)
            {
                regle.SousRegles = regle.ReglesId.Select(id => GetRegle(id)).ToArray();
            }
        }
    }
}
