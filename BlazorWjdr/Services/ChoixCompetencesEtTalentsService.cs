namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ChoixCompetencesEtTalentsService
    {
        private readonly CompetencesEtTalentsService _competencesEtTalentsService;
        
        private Dictionary<int, CompetenceDto[]>? _cacheChoixCompetences;
        private Dictionary<int, TalentDto[]>? _cacheChoixTalents;

        public ChoixCompetencesEtTalentsService(CompetencesEtTalentsService competencesEtTalentsService)
        {
            _competencesEtTalentsService = competencesEtTalentsService;
        }

        public IEnumerable<CompetenceDto[]> GetChoixCompetences(IEnumerable<int> ids) => ids.Select(GetChoixCompetence).ToArray();

        private CompetenceDto[] GetChoixCompetence(int id)
        {
            if (_cacheChoixCompetences == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheChoixCompetences[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public IEnumerable<TalentDto[]> GetChoixTalents(IEnumerable<int> ids) => ids.Select(GetChoixTalent).ToArray();

        private TalentDto[] GetChoixTalent(int id)
        {
            if (_cacheChoixTalents == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheChoixTalents[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private void Initialize()
        {
            var allChoixTalents = DataSource.JsonLoader
                .GetRootChoixTalent()
                .items;

            _cacheChoixTalents = allChoixTalents.ToDictionary(k => k.id, v => v.choixtalentkeys
                .Select(id => _competencesEtTalentsService.GetTalent(id))
                .OrderBy(t => t.Nom)
                .ToArray());

            var allChoixCompetences = DataSource.JsonLoader
                .GetRootChoixCompetence()
                .items;

            _cacheChoixCompetences = allChoixCompetences.ToDictionary(k => k.id, v => v.choixcompetencekeys
                .Select(id => _competencesEtTalentsService.GetCompetence(id))
                .OrderBy(c => c.Nom)
                .ToArray());
        }
    }
}
