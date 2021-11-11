// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    public class JsonUser
    {
        public int id { get; set; }
        public string email { get; set; } = null!;
        public string pseudo { get; set; } = null!;
    }

    public class JsonCampagne
    {
        public int id { get; set; }
        public string titre { get; set; } = null!;
        public int mj { get; set; }
        public int team { get; set; }
        public JsonSeance[]? seances { get; set; }
    }

    public class JsonTeam
    {
        public int id { get; set; }
        public string nom { get; set; } = null!;
    }

    public class JsonFact
    {
        public int tri { get; set; }
        public int[]? pjs { get; set; }
        public string fact { get; set; } = null!;
    }

    public class JsonRencontre
    {
        public string groupe { get; init; } = null!;
        public JsonCombattant[]? ennemis { get; set; }
        public JsonCombattant[]? allies { get; set; }
    }
    
    public class JsonCombattant
    {
        public int id { get; set; }
        public string? nom { get; set; }
    }

    public class JsonSeance
    {
        public string quand { get; set; } = null!;
        public bool secret { get; set; }
        public int acte { get; set; }
        public int duree { get; set; }
        public string titre { get; set; } = null!;
        public string? scenario { get; set; } = null!;
        public int xp { get; set; }
        public string? xp_comment { get; set; }
        public string? resume { get; set; }
        public int[]? lieux { get; set; }
        public int[]? pjs { get; set; }
        public JsonFact[]? facts { get; set; }
        public JsonRencontre[]? rencontres { get; set; }
    }

    public class RootCampagne
    {
        public JsonUser[] users { get; set; } = null!;
        public JsonCampagne[] campagnes { get; set; } = null!;
        public JsonTeam[] teams { get; set; } = null!;
    }
}
