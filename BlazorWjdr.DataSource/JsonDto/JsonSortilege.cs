namespace BlazorWjdr.DataSource.JsonDto;

public record JsonSortilege(
    int id,
    int[]? aptitudes,
    string nom,
    string type,
    string distance,
    string cible,
    string duree,
    string? ingredient,
    string effet,
    int? ns);

public record RootSortilege(JsonSortilege[] sortileges);