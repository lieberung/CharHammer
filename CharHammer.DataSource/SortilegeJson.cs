using System.Collections.Generic;

namespace CharHammer.DataSource;

public record SortilegeJson(
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

public record RootSortilege(ICollection<SortilegeJson> sortileges);