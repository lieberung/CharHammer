using System.Collections.Generic;

namespace CharHammer.DataSource;

public record RegleJson(
    int id,
    bool html,
    string titre,
    string regle,
    int[]? regles,
    int[]? carrieres,
    int[]? aptitudes,
    int[][]? aptitudes_choix,
    int[]? bestioles,
    int[]? tables,
    int[]? lieux);

public record RootRegle(IEnumerable<RegleJson> items);