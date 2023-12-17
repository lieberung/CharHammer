using System.Collections.Generic;

namespace CharHammer.DataSource;

public record DataJson(
    ICollection<AptitudeJson> aptitudes,
    ICollection<ArmeAttributJson> attributs,
    ICollection<ArmeJson> armes,
    ICollection<ArmureJson> armures,
    ICollection<UserJson> users,
    ICollection<CampagneJson> campagnes,
    ICollection<TeamJson> teams,
    ICollection<CarriereJson> carrieres,
    ICollection<DomaineJson> domaines,
    ICollection<ChronoJson> chrono,
    ICollection<CreatureJson> creatures,
    ICollection<DieuJson> dieux,
    ICollection<EquipementJson> equipements,
    ICollection<LieuTypeJson> lieuxtypes,
    ICollection<LieuJson> lieux,
    ICollection<RaceJson> races,
    ICollection<ReferenceJson> references,
    ICollection<RegleJson> regles,
    ICollection<ScenarioJson> scenarios,
    ICollection<SortilegeJson> sortileges,
    ICollection<TableJson> tables
);