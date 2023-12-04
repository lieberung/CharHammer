// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public class JsonCarriere
{
    public int id { get; set; }
    public int? parent { get; set; }
    public string nom { get; set; } = null!;
    public string? nom_en { get; set; }
    public string? revenu { get; set; }
    public int? niveau { get; set; }
    public string? groupe { get; set; }
    public string? restriction { get; set; }
    public JsonSource? source { get; set; }
    public int[]? avancements { get; set; }
    public int[]? debouch { get; set; }
    public JsonCarriereInitiale[]? tirage { get; set; }
    public JsonProfil profil { get; set; } = null!;
    public int? metier { get; set; }
    public int[]? aptitudes { get; set; }
    public int[][]? aptitudes_choix { get; set; }
    public string? leitmotiv { get; set; }
    public string description { get; set; } = null!;
    public JsonCitation[]? ambiance { get; set; }
    public string dotations { get; set; } = null!;
}

public class JsonCarriereInitiale
{
    public int r { get; set; }
    public int f { get; set; }
}

public class JsonSource
{
    public int? id { get; set; }
    public string? info { get; set; }
}

//public record JsonCitation(string c, string? a, string? s);
public class JsonCitation { public string? c { get; set; } public string? a { get; set; } public string? s { get; set; } }

public class RootCarriere
{
    public List<JsonCarriere> items { get; set; } = null!;
}