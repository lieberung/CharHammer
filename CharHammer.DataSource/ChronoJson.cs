namespace CharHammer.DataSource;

using System.Collections.Generic;

public record DomaineJson(int id, string nom);

public record ChronoJson(
    int debut,
    int? fin,
    string resume,
    string? titre,
    string? comment,
    ICollection<int>? sources,
    ICollection<int>? domaines
);

public record RootChrono(ICollection<DomaineJson> domaines, ICollection<ChronoJson> chrono);
