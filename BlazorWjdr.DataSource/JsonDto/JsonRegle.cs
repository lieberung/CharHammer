// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
using System.Collections.Generic;

namespace BlazorWjdr.DataSource.JsonDto;

public class JsonRegle
{
    public int id { get; set; }
    public bool html { get; set; }
    public string titre { get; set; } = null!;
    public string regle { get; set; } = null!;
    public int[]? regles { get; set; }
    public int[]? carrieres { get; set; }
    public int[]? aptitudes { get; set; }
    public int[][]? aptitudes_choix { get; set; }
    public int[]? bestioles { get; set; }
    public int[]? tables { get; set; }
    public int[]? lieux { get; set; }
}

public class RootRegle
{
    public List<JsonRegle> items { get; set; } = null!;
}