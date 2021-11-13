using BlazorWjdr.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Services
{
    public class ScenariosService
    {
        private readonly ScenarioDto[] _scenarios;

        public ScenariosService(IEnumerable<ScenarioDto> scenarios)
        {
            _scenarios = scenarios.OrderBy(s => s.Nom).ToArray();
        }

        public IEnumerable<ScenarioDto> AllScenarios(bool pasDeDaubes, string filtre)
        {
            filtre = GenericService.NettoyerPourRecherche(filtre);
            return _scenarios.Where(s =>
                (s.Note is 0 or > 2 || pasDeDaubes == false) && (filtre == "" || GenericService.NettoyerPourRecherche(s.Nom).Contains(filtre)));
        }
        public IEnumerable<ScenarioDto> AllScenarios(LieuDto[] lieux, LieuTypeDto[] typesDeLieux)
        {
            var tousLesTypes = new List<LieuTypeDto>(typesDeLieux);
            tousLesTypes.AddRange(lieux.Select(l => l.TypeDeLieu).Distinct());
            return _scenarios.Where(s => s.Lieux.Intersect(lieux).Any() || s.LieuxTypes.Intersect(tousLesTypes).Any());
        }
    }
}
