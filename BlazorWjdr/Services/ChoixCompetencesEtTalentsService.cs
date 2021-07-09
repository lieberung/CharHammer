namespace BlazorWjdr.Services
{
    using BlazorWjdr.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ChoixCompetencesEtTalentsService
    {
        private readonly CompetencesEtTalentsService _competencesEtTalentsService;
        
        private Dictionary<int, CompetenceDto[]>? _cacheChoixCompetences = null;
        private Dictionary<int, TalentDto[]>? _cacheChoixTalents = null;

        public ChoixCompetencesEtTalentsService(CompetencesEtTalentsService competencesEtTalentsService)
        {
            _competencesEtTalentsService = competencesEtTalentsService;
        }

        public CompetenceDto[][] GetChoixCompetences(int[] ids) => ids.Select(id => GetChoixCompetence(id)).ToArray();
        public CompetenceDto[] GetChoixCompetence(int id)
        {
            if (_cacheChoixCompetences == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheChoixCompetences[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public TalentDto[][] GetChoixTalents(int[] ids) => ids.Select(id => GetChoixTalent(id)).ToArray();
        public TalentDto[] GetChoixTalent(int id)
        {
            if (_cacheChoixTalents == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheChoixTalents[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private void Initialize()
        {
            var _allChoixTalents = DataSource.JsonLoader
                .GetRootChoixTalent()
                .items;

            _cacheChoixTalents = _allChoixTalents.ToDictionary(k => k.id, v => v.choixtalentkeys
                .Select(id => _competencesEtTalentsService.GetTalent(id))
                .OrderBy(t => t.Nom)
                .ToArray());

            var _allChoixCompetences = DataSource.JsonLoader
                .GetRootChoixCompetence()
                .items;

            _cacheChoixCompetences = _allChoixCompetences.ToDictionary(k => k.id, v => v.choixcompetencekeys
                .Select(id => _competencesEtTalentsService.GetCompetence(id))
                .OrderBy(c => c.Nom)
                .ToArray());
        }
    }
}
