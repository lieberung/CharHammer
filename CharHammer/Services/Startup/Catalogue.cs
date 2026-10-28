using CharHammer.DataSource;
using System.Diagnostics;

namespace CharHammer.Services.Startup;

/// <summary>
/// Contient les services de données, construits une fois les données chargées par <see cref="App"/>.
/// Les services sont déclarés dans l'injection de dépendances avant le chargement (voir <see cref="Configuration"/>)
/// et lus ici au premier usage : aucune page ne s'affiche avant que le catalogue soit prêt.
/// </summary>
public sealed class Catalogue
{
    public bool EstCharge { get; private set; }

    public AptitudesService Aptitudes { get; private set; } = null!;
    public LieuxService Lieux { get; private set; } = null!;
    public DieuxService Dieux { get; private set; } = null!;
    public ReferencesService References { get; private set; } = null!;
    public TablesService Tables { get; private set; } = null!;
    public ChronologieService Chronologie { get; private set; } = null!;
    public ArmesService Armes { get; private set; } = null!;
    public CarrieresService Carrieres { get; private set; } = null!;
    public RacesService Races { get; private set; } = null!;
    public TableDesCarrieresInitialesService TablesDesCarrieresInitiales { get; private set; } = null!;
    public BestiolesService Bestioles { get; private set; } = null!;
    public ReglesService Regles { get; private set; } = null!;
    public SortilegesService Sortileges { get; private set; } = null!;
    public CampagnesService Campagnes { get; private set; } = null!;
    public ScenariosService Scenarios { get; private set; } = null!;

    /// <summary>Le catalogue, à condition qu'il soit chargé.</summary>
    public Catalogue Exiger()
        => EstCharge ? this : throw new InvalidOperationException("Les données ne sont pas encore chargées : aucun service de données n'est disponible.");

    /// <summary>Construit le graphe d'objets à partir des données brutes, puis les services (≈ 1 s en WebAssembly).</summary>
    public void Initialiser(DataJson data)
    {
        Console.Write("Initializing data... ");
        var chrono = Stopwatch.StartNew();

        var dataUsers = Initializer.InitializeUsers(data.users);
        var dataAptitudes = Initializer.InitializeAptitudes(data.aptitudes);
        var dataReferences = Initializer.InitializeReferences(data.references);
        var dataDomaines = Initializer.InitializeDomaines(data.domaines);
        var dataChrono = Initializer.InitializeChronologie(data.chrono, dataReferences, dataDomaines);
        var dataLieuxTypes = Initializer.InitializeLieuxTypes(data.lieuxtypes);
        var dataLieux = Initializer.InitializeLieux(data.lieux, dataLieuxTypes);
        var dataEquipements = Initializer.InitializeEquipements(data.equipements, dataLieux, dataLieuxTypes);
        var dataDieux = Initializer.InitializeDieux(data.dieux, dataAptitudes, dataLieux);
        var dataTables = Initializer.InitializeTables(data.tables);
        var dataArmesAttributs = Initializer.InitializeArmesAttributs(data.attributs);
        var dataArmes = Initializer.InitializeArmes(data.armes, dataArmesAttributs, dataAptitudes);
        var dataArmures = Initializer.InitializeArmures(data.armures, dataArmesAttributs);
        var dataSortileges = Initializer.InitializeSortileges(data.sortileges, dataAptitudes);
        var dataRaces = Initializer.InitializeRaces(data.races, dataAptitudes, dataLieux);
        var dataCarrieres = Initializer.InitializeCarrieres(data.carrieres, dataRaces, dataAptitudes, dataReferences);
        var dataTablesCarrInit = Initializer.InitializeTablesCarrieresInitiales(dataCarrieres);
        var dataBestioles = Initializer.InitializeCreatures(data.creatures, dataRaces, dataAptitudes, dataLieux, dataCarrieres, dataArmes, dataArmures, dataEquipements, dataSortileges, dataUsers);
        var dataRegles = Initializer.InitializeRegles(data.regles, dataTables, dataBestioles, dataAptitudes, dataLieux, dataCarrieres);
        var dataScenarios = Initializer.InitializeScenarios(data.scenarios, dataLieux, dataLieuxTypes);
        var dataTeams = Initializer.InitializeTeams(data.teams);
        var dataCampagnes = Initializer.InitializeCampagnes(dataUsers, dataTeams, data.campagnes, dataScenarios, dataBestioles, dataCarrieres, dataLieux);

        Aptitudes = new AptitudesService(dataAptitudes);
        Lieux = new LieuxService(dataLieuxTypes, dataLieux);
        Dieux = new DieuxService(dataDieux);
        References = new ReferencesService(dataReferences);
        Tables = new TablesService(dataTables);
        Chronologie = new ChronologieService(dataChrono, dataDomaines);
        Armes = new ArmesService(dataArmesAttributs, dataArmes, dataArmures, dataEquipements);
        Carrieres = new CarrieresService(dataCarrieres);
        Races = new RacesService(dataRaces);
        TablesDesCarrieresInitiales = new TableDesCarrieresInitialesService(dataTablesCarrInit);
        Bestioles = new BestiolesService(dataBestioles);
        Regles = new ReglesService(dataRegles);
        Sortileges = new SortilegesService(dataSortileges);
        Campagnes = new CampagnesService(dataCampagnes);
        Scenarios = new ScenariosService(dataScenarios);

        EstCharge = true;
        Console.WriteLine($"{chrono.ElapsedMilliseconds} ms.");
    }
}
