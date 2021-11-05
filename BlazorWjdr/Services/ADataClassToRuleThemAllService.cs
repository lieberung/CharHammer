using System;
using System.Data;
using BlazorWjdr.DataSource.JsonDto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWjdr.Services
{
    public class ADataClassToRuleThemAllService
    {
        private readonly HttpClient _httpClient;

        public ADataClassToRuleThemAllService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task InitializeDataAsync()
        {
            var startTime = DateTime.Now;
            Aptitudes = await GetRootAptitude();
            Armes = await GetRootArme();
            Campagne = await GetRootCampagne();
            Creatures = await GetRootCreature();
            Carrieres = await GetRootCarriere();
            Chrono = await GetRootChrono();
            Dieux = await GetRootDieu();
            Equipements = await GetRootEquipement();
            Lieux = await GetRootLieu();
            Races = await GetRootRace();
            References = await GetRootReference();
            Regles = await GetRootRegle();
            Sortileges = await GetRootSortilege();
            Tables = await GetRootTable();
            Scenarios = await GetRootScenarios();
            Console.WriteLine($"Loading json data... {DateTime.Now.Subtract(startTime).TotalSeconds}sec.");
        }

        public RootAptitude? Aptitudes { get; private set; }
        public RootCampagne? Campagne { get; private set; }
        public RootScenario? Scenarios { get; private set; }
        public RootCreature? Creatures { get; private set; }
        public RootCarriere? Carrieres { get; private set; }
        public RootChrono? Chrono { get; private set; }
        public RootDieu? Dieux { get; private set; }
        public RootLieu? Lieux { get; private set; }
        public RootArme? Armes { get; private set; }
        public RootRace? Races { get; private set; }
        public RootSortilege? Sortileges { get; private set; }
        public RootReference? References { get; private set; }
        public RootTable? Tables { get; private set; }
        public RootRegle? Regles { get; private set; }
        public RootEquipement? Equipements { get; private set; }

        private const string JsonDataPath = "json-data";

        private async Task<T> LoadRootFromJson<T>(string path)
        {
            var data = await _httpClient.GetFromJsonAsync<T>(path);
            return data ?? throw new NoNullAllowedException(nameof(data));
        }

        private async Task<RootAptitude> GetRootAptitude() => await LoadRootFromJson<RootAptitude>($"{JsonDataPath}/aptitude.json");
        private async Task<RootEquipement> GetRootEquipement() => await LoadRootFromJson<RootEquipement>($"{JsonDataPath}/equipement.json");
        private async Task<RootCampagne> GetRootCampagne() => await LoadRootFromJson<RootCampagne>($"{JsonDataPath}/campagne.json");
        private async Task<RootCreature> GetRootCreature() => await LoadRootFromJson<RootCreature>($"{JsonDataPath}/creature.json");
        private async Task<RootCarriere> GetRootCarriere() => await LoadRootFromJson<RootCarriere>($"{JsonDataPath}/carriere.json");
        private async Task<RootChrono> GetRootChrono() => await LoadRootFromJson<RootChrono>($"{JsonDataPath}/chrono.json");
        private async Task<RootDieu> GetRootDieu() => await LoadRootFromJson<RootDieu>($"{JsonDataPath}/dieu.json");
        private async Task<RootLieu> GetRootLieu() => await LoadRootFromJson<RootLieu>($"{JsonDataPath}/lieu.json");
        private async Task<RootArme> GetRootArme() => await LoadRootFromJson<RootArme>($"{JsonDataPath}/arme.json");
        private async Task<RootRace> GetRootRace() => await LoadRootFromJson<RootRace>($"{JsonDataPath}/race.json");
        private async Task<RootSortilege> GetRootSortilege() => await LoadRootFromJson<RootSortilege>($"{JsonDataPath}/sortilege.json");
        private async Task<RootReference> GetRootReference() => await LoadRootFromJson<RootReference>($"{JsonDataPath}/reference.json");
        private async Task<RootTable> GetRootTable() => await LoadRootFromJson<RootTable>($"{JsonDataPath}/table.json");
        private async Task<RootRegle> GetRootRegle() => await LoadRootFromJson<RootRegle>($"{JsonDataPath}/regle.json");
        private async Task<RootScenario> GetRootScenarios() => await LoadRootFromJson<RootScenario>($"{JsonDataPath}/scenarios.json");
    }
}
