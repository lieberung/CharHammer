namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonChrono(
    int debut,
    int? fin,
    string resume,
    string? titre,
    string? comment,
    List<int> sources,
    List<int> domaines
);

public record RootChrono(List<JsonDomaine> domaines, List<JsonChrono> items);
