namespace CharHammer.DataSource;

public record DataJson(
    AptitudeJson[] aptitudes,
    ArmeAttributJson[] attributs,
    ArmeJson[] armes,
    ArmureJson[] armures,
    UserJson[] users,
    CampagneJson[] campagnes,
    TeamJson[] teams,
    CarriereJson[] carrieres,
    DomaineJson[] domaines,
    ChronoJson[] chrono,
    CreatureJson[] creatures,
    DieuJson[] dieux,
    EquipementJson[] equipements,
    LieuTypeJson[] lieuxtypes,
    LieuJson[] lieux,
    RaceJson[] races,
    ReferenceJson[] references,
    RegleJson[] regles,
    ScenarioJson[] scenarios,
    SortilegeJson[] sortileges,
    TableJson[] tables
);