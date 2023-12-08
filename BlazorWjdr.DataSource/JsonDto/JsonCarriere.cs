namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonCarriere(
    int id,
    int? parent,
    string nom,
    string? nom_en,
    string? revenu,
    int? niveau,
    string? groupe,
    string? restriction,
    JsonSource? source,
    int[]? avancements,
    int[]? debouch,
    JsonCarriereInitiale[]? tirage,
    JsonProfil profil,
    int? metier,
    int[]? aptitudes,
    int[][]? aptitudes_choix,
    string? leitmotiv,
    string description,
    JsonCitation[]? ambiance,
    string dotations);

public record JsonCarriereInitiale(int r, int f);

public record JsonSource(int? id, string? info);

public record JsonCitation(string c, string? a, string? s);

public record RootCarriere(List<JsonCarriere> items);