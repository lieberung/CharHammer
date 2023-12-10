namespace CharHammer.DataSource;

using System.Collections.Generic;

public record ChronoJson(
    int debut,
    int? fin,
    string resume,
    string? titre,
    string? comment,
    IEnumerable<int> sources,
    IEnumerable<int> domaines
);

public record RootChrono(IEnumerable<DomaineJson> domaines, IEnumerable<ChronoJson> items);
