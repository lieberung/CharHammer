namespace CharHammer.DataSource;

using System.Collections.Generic;

public record RaceJson(
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
    OpinionJson[]? opinions,
    InfoJson[]? infos);

public record OpinionJson(int race, string ambiance);

public record InfoJson(string? titre, string detail);

public record RootRace(ICollection<RaceJson> items);