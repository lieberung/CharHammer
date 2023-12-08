using System;
using BlazorWjdr.DataSource.JsonDto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWjdr.Services;

public class ADataClassToRuleThemAllService
{
    private readonly HttpClient _httpClient; //ToDo: HttpClientFactory

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
        return data ?? throw new ArgumentNullException(nameof(data));
    }

    private Task<RootAptitude> GetRootAptitude() => LoadRootFromJson<RootAptitude>($"{JsonDataPath}/aptitude.json");
    private Task<RootEquipement> GetRootEquipement() => LoadRootFromJson<RootEquipement>($"{JsonDataPath}/equipement.json");
    private Task<RootCampagne> GetRootCampagne() => LoadRootFromJson<RootCampagne>($"{JsonDataPath}/campagne.json");
    private Task<RootCreature> GetRootCreature() => LoadRootFromJson<RootCreature>($"{JsonDataPath}/creature.json");
    private Task<RootCarriere> GetRootCarriere() => LoadRootFromJson<RootCarriere>($"{JsonDataPath}/carriere.json");
    private Task<RootChrono> GetRootChrono() => LoadRootFromJson<RootChrono>($"{JsonDataPath}/chrono.json");
    private Task<RootDieu> GetRootDieu() => LoadRootFromJson<RootDieu>($"{JsonDataPath}/dieu.json");
    private Task<RootLieu> GetRootLieu() => LoadRootFromJson<RootLieu>($"{JsonDataPath}/lieu.json");
    private Task<RootArme> GetRootArme() => LoadRootFromJson<RootArme>($"{JsonDataPath}/arme.json");
    private Task<RootRace> GetRootRace() => LoadRootFromJson<RootRace>($"{JsonDataPath}/race.json");
    private Task<RootSortilege> GetRootSortilege() => LoadRootFromJson<RootSortilege>($"{JsonDataPath}/sortilege.json");
    private Task<RootReference> GetRootReference() => LoadRootFromJson<RootReference>($"{JsonDataPath}/reference.json");
    private Task<RootTable> GetRootTable() => LoadRootFromJson<RootTable>($"{JsonDataPath}/table.json");
    private Task<RootRegle> GetRootRegle() => LoadRootFromJson<RootRegle>($"{JsonDataPath}/regle.json");
    private Task<RootScenario> GetRootScenarios() => LoadRootFromJson<RootScenario>($"{JsonDataPath}/scenarios.json");

    public void Dispose()
    {
        Aptitudes!.items.Clear();
        //Armes!.armes.Clear();
        //Armes.armures.Clear();
        // Campagne!.campagnes = Array.Empty<JsonCampagne>();
        // Campagne.teams = Array.Empty<JsonTeam>();
        // Campagne.users = Array.Empty<JsonUser>();
        Creatures!.items.Clear();
        Carrieres!.items.Clear();
        Chrono!.items.Clear();
        //Dieux!.items.Clear();
        Equipements!.items.Clear();
        Lieux!.items.Clear();
        Races!.items.Clear();
        References!.items.Clear();
        Regles!.items.Clear();
        //Sortileges!.sortileges = Array.Empty<JsonSortilege>();
        Tables!.items.Clear();
        //Scenarios!.scenarios = Array.Empty<JsonScenario>();
        _httpClient.Dispose();
    }
}
