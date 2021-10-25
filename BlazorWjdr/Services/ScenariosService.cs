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

        public IEnumerable<ScenarioDto> AllScenarios() => _scenarios;
    }
}
