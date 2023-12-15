namespace CharHammer.DataSource;

using System.Collections.Generic;

public record AptitudeJson(
    int id,
    int? parent,
    string categ,
    string? categ_spe,
    string nom,
    string? nom_en,
    string? spe,
    ICollection<int>? aptitudes,
    ICollection<int>? incompatibles,
    bool ignorer,
    bool martial,
    string? carac,
    string? resume,
    string? description,
    string? max,
    string? tests,
    int? severite,
    string? guerison,
    bool? contagieux);

public record RootAptitude(ICollection<AptitudeJson> items);