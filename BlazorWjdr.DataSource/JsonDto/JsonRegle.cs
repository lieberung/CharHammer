using System.Collections.Generic;

namespace BlazorWjdr.DataSource.JsonDto;

public record JsonRegle(
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

public record RootRegle(List<JsonRegle> items);