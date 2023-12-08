namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonRace(
    int id,
    int? parent,
    int[]? lieux,
    int[]? aptitudes,
    bool pj,
    bool group_only,
    string nom_masculin,
    string? nom_feminin,
    string? nom_autoch,
    string? description,
    JsonOpinion[]? opinions,
    JsonInfo[]? infos);

public record JsonOpinion(int race, string ambiance);

public record JsonInfo(string? titre, string detail);

public record RootRace(List<JsonRace> items);