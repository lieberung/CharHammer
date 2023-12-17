using CharHammer.DataSource;
using CharHammer.Services;

namespace CharHammer.Services.Startup;

public static class Configuration
{
    public static void ConfigureServices(this IServiceCollection services, DataJson data)
    {
        Console.Write("Initializing data... ");
        var startTime = DateTime.Now;
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

        Console.WriteLine($"{DateTime.Now.Subtract(startTime).TotalSeconds}sec.");

        services.AddSingleton(_ => new AptitudesService(dataAptitudes));
        services.AddSingleton(_ => new LieuxService(dataLieuxTypes, dataLieux));
        services.AddSingleton(_ => new DieuxService(dataDieux));
        services.AddSingleton(_ => new ReferencesService(dataReferences));
        services.AddSingleton(_ => new TablesService(dataTables));
        services.AddSingleton(_ => new ChronologieService(dataChrono, dataDomaines));
        services.AddSingleton(_ => new ArmesService(dataArmesAttributs, dataArmes, dataArmures, dataEquipements));
        services.AddSingleton(_ => new CarrieresService(dataCarrieres));
        services.AddSingleton(_ => new RacesService(dataRaces));
        services.AddSingleton(_ => new TableDesCarrieresInitialesService(dataTablesCarrInit));
        services.AddSingleton(_ => new BestiolesService(dataBestioles));
        services.AddSingleton(_ => new ReglesService(dataRegles));
        services.AddSingleton(_ => new SortilegesService(dataSortileges));
        services.AddSingleton(_ => new CampagnesService(dataCampagnes));
        services.AddSingleton(_ => new ScenariosService(dataScenarios));

        services.AddSingleton(_ => new AppState());
    }
}
