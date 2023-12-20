namespace CharHammer.DataSource;

public record AptitudeJson(
    int id,
    int? parent,
    string categ,
    string? categ_spe,
    string nom,
    string? nom_en,
    string? spe,
    int[]? aptitudes,
    int[]? incompatibles,
    bool ignorer,
    bool martial,
    string? carac,
    string? resume,
    string? description,
    string? max,
    string? tests,
    int? severite,
    string? guerison,
    bool? contagieux);

public record RootAptitude(AptitudeJson[] aptitudes);