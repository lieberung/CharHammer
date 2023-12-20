namespace CharHammer.DataSource;

public record DomaineJson(int id, string nom);

public record ChronoJson(
    int debut,
    int? fin,
    string resume,
    string? titre,
    string? comment,
    int[]? sources,
    int[]? domaines
);

public record RootChrono(DomaineJson[] domaines, ChronoJson[] chrono);
