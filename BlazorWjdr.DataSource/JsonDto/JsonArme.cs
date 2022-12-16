// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public class JsonArmeAttribut
{
    public int id { get; set; }
    public string type { get; set; } = null!;
    public string nom { get; set; } = null!;
    public string description { get; set; } = null!;
}

public class JsonArme
{
    public int id { get; set; }
    public int? parent { get; set; }
    public int[] competences { get; set; } = null!;
    public int[]? groupes { get; set; }
    public int[]? attributs { get; set; } = null!;
    public string nom { get; set; } = null!;
    public string degats { get; set; } = null!;
    public string? allonge { get; set; }
    public string? portee { get; set; }
    public string? rechargement { get; set; }
    public string enc { get; set; } = null!;
    public string prix { get; set; } = null!;
    public string dispo { get; set; } = null!;
    public string? description { get; set; } = null!;
}

public class JsonArmure
{
    public int id { get; set; }
    public int? parent { get; set; }
    public string nom { get; set; } = null!;
    public string type { get; set; } = null!;
    public string pa { get; set; } = null!;
    public string zones { get; set; } = null!;
    public string prix { get; set; } = null!;
    public string enc { get; set; } = null!;
    public string dispo { get; set; } = null!;
    public string description { get; set; } = null!;
    public int[]? attributs { get; set; }
}

public class RootArme
{
    public List<JsonArmeAttribut> attributs { get; set; } = null!;
    public List<JsonArme> armes { get; set; } = null!;
    public List<JsonArmure> armures { get; set; } = null!;
}
