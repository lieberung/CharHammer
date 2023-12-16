using CharHammer.DataSource;
using System.Net.Http.Json;

namespace CharHammer.Services;

public class ADataClassToRuleThemAllService(HttpClient httpClient)
{
    public async Task InitializeDataAsync()
    {
        Console.Write("Loading json data... ");
        var startTime = DateTime.Now;
        Aptitudes = await GetAptitudes();
        Armes = await GetArmes();
        Campagne = await GetCampagnes();
        Creatures = await GetCreatures();
        Carrieres = await GetCarrieres();
        Chrono = await GetChrono();
        Dieux = await GetDieux();
        Equipements = await GetEquipement();
        Lieux = await GetLieux();
        Races = await GetRaces();
        References = await GetReferences();
        Regles = await GetRegles();
        Sortileges = await GetSortileges();
        Tables = await GetTables();
        Scenarios = await GetScenarios();
        Console.WriteLine($"{DateTime.Now.Subtract(startTime).TotalSeconds}sec.");
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
        var data = await httpClient.GetFromJsonAsync<T>(path);
        ArgumentNullException.ThrowIfNull(data);
        return data;
    }

    private Task<RootAptitude> GetAptitudes() => LoadRootFromJson<RootAptitude>($"{JsonDataPath}/aptitude.json");
    private Task<RootEquipement> GetEquipement() => LoadRootFromJson<RootEquipement>($"{JsonDataPath}/equipement.json");
    private Task<RootCampagne> GetCampagnes() => LoadRootFromJson<RootCampagne>($"{JsonDataPath}/campagne.json");
    private Task<RootCreature> GetCreatures() => LoadRootFromJson<RootCreature>($"{JsonDataPath}/creature.json");
    private Task<RootCarriere> GetCarrieres() => LoadRootFromJson<RootCarriere>($"{JsonDataPath}/carriere.json");
    private Task<RootChrono> GetChrono() => LoadRootFromJson<RootChrono>($"{JsonDataPath}/chrono.json");
    private Task<RootDieu> GetDieux() => LoadRootFromJson<RootDieu>($"{JsonDataPath}/dieu.json");
    private Task<RootLieu> GetLieux() => LoadRootFromJson<RootLieu>($"{JsonDataPath}/lieu.json");
    private Task<RootArme> GetArmes() => LoadRootFromJson<RootArme>($"{JsonDataPath}/arme.json");
    private Task<RootRace> GetRaces() => LoadRootFromJson<RootRace>($"{JsonDataPath}/race.json");
    private Task<RootSortilege> GetSortileges() => LoadRootFromJson<RootSortilege>($"{JsonDataPath}/sortilege.json");
    private Task<RootReference> GetReferences() => LoadRootFromJson<RootReference>($"{JsonDataPath}/reference.json");
    private Task<RootTable> GetTables() => LoadRootFromJson<RootTable>($"{JsonDataPath}/table.json");
    private Task<RootRegle> GetRegles() => LoadRootFromJson<RootRegle>($"{JsonDataPath}/regle.json");
    private Task<RootScenario> GetScenarios() => LoadRootFromJson<RootScenario>($"{JsonDataPath}/scenarios.json");
}
