namespace CharHammer.DataSource;

using System.Collections.Generic;

public record CarriereJson(
    int id,
    int? parent,
    string nom,
    string? nom_en,
    string? revenu,
    int? niveau,
    string? groupe,
    string? restriction,
    SourceJson? source,
    int[]? avancements,
    int[]? debouch,
    CarriereInitialeJson[]? tirage,
    ProfilJson profil,
    int? metier,
    int[]? aptitudes,
    int[][]? aptitudes_choix,
    string? leitmotiv,
    string description,
    CitationJson[]? ambiance,
    string dotations);

public record CarriereInitialeJson(int r, int f);

public record SourceJson(int? id, string? info);

public record CitationJson(string c, string? a, string? s);

public record RootCarriere(IEnumerable<CarriereJson> items);