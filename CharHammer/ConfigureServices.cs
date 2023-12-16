using CharHammer.Services;

namespace CharHammer;

public static class Configuration
{
    public static void ConfigureServices(this IServiceCollection services, ADataClassToRuleThemAllService data)
    {
        Console.Write("Initializing data... ");
        var startTime = DateTime.Now;
        var dataUsers = Initializer.InitializeUsers(data.Campagne!.users);

        var dataAptitudes = Initializer.InitializeAptitudes(data.Aptitudes!.aptitudes);
        var dataReferences = Initializer.InitializeReferences(data.References!.references);
        var dataDomaines = Initializer.InitializeDomaines(data.Chrono!.domaines);
        var listChrono = Initializer.InitializeChronologie(data.Chrono!.chrono, dataReferences, dataDomaines);
        var dataLieuxTypes = Initializer.InitializeLieuxTypes(data.Lieux!.types);
        var dataLieux = Initializer.InitializeLieux(data.Lieux!.items, dataLieuxTypes);
        var dataEquipements = Initializer.InitializeEquipements(data.Equipements!.items, dataLieux, dataLieuxTypes);
        var dataDieux = Initializer.InitializeDieux(data.Dieux!.items, dataAptitudes, dataLieux);
        var dataTables = Initializer.InitializeTables(data.Tables!.items);
        var dataArmesAttributs = Initializer.InitializeArmesAttributs(data.Armes!.attributs);
        var dataArmes = Initializer.InitializeArmes(data.Armes!.armes, dataArmesAttributs, dataAptitudes);
        var dataArmures = Initializer.InitializeArmures(data.Armes!.armures, dataArmesAttributs);
        var dataSortileges = Initializer.InitializeSortileges(data.Sortileges!.sortileges, dataAptitudes);
        var dataRaces = Initializer.InitializeRaces(data.Races!.items, dataAptitudes, dataLieux);
        var dataCarrieres = Initializer.InitializeCarrieres(data.Carrieres!.items, dataRaces, dataAptitudes, dataReferences);
        var dataTablesCarrInit = Initializer.InitializeTablesCarrieresInitiales(dataCarrieres);
        var dataBestioles = Initializer.InitializeCreatures(data.Creatures!.items, dataRaces, dataAptitudes, dataLieux, dataCarrieres, dataArmes, dataArmures, dataEquipements, dataSortileges, dataUsers);
        var dataRegles = Initializer.InitializeRegles(data.Regles!.items, dataTables, dataBestioles, dataAptitudes, dataLieux, dataCarrieres);

        var dataScenarios = Initializer.InitializeScenarios(data.Scenarios!.scenarios, dataLieux, dataLieuxTypes);
        var dataTeams = Initializer.InitializeTeams(data.Campagne!.teams);
        var dataCampagnes = Initializer.InitializeCampagnes(dataUsers, dataTeams, data.Campagne!.campagnes, dataScenarios, dataBestioles, dataCarrieres, dataLieux);

        Console.WriteLine($"{DateTime.Now.Subtract(startTime).TotalSeconds}sec.");

        services.AddSingleton(_ => new AptitudesService(dataAptitudes));
        services.AddSingleton(_ => new LieuxService(dataLieuxTypes, dataLieux));
        services.AddSingleton(_ => new DieuxService(dataDieux));
        services.AddSingleton(_ => new ReferencesService(dataReferences));
        services.AddSingleton(_ => new TablesService(dataTables));
        services.AddSingleton(_ => new ChronologieService(listChrono, dataDomaines));
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
