// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonRace
    {
        public int id { get; set; }
        public int? parent { get; set; }
        public int[]? lieux { get; set; }
        public bool pj { get; set; }
        public bool group_only { get; set; }
        public string nom_masculin { get; set; } = null!;
        public string nom_feminin { get; set; } = null!;
        public string description { get; set; } = null!;
        public JsonOpinion[]? opinions { get; set; }
    }

    public class JsonOpinion
    {
        public int race { get; set; }
        public string ambiance { get; set; }
    }
    
    public class RootRace
    {
        public List<JsonRace> items { get; set; } = null!;
    }

}
