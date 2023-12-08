namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonAptitude(
    int id,
    int? parent,
    string categ,
    string? categ_spe,
    string nom,
    string? nom_en,
    string? spe,
    List<int>? aptitudes,
    List<int>? incompatibles,
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

public record RootAptitude(List<JsonAptitude> items);