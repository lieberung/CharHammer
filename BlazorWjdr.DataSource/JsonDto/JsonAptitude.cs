// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonAptitude
    {
        public int id { get; set; }
        public int? parent { get; set; }

        public string categ { get; set; } = null!; // skill / talent / trait
        public string? categ_spe { get; set; }
        public string nom { get; set; } = null!;
        public string? nom_en { get; set; }
        public string? spe { get; set; }

        public List<int>? aptitudes { get; set; }
        public List<int>? incompatibles { get; set; }

        public bool ignorer { get; set; }
        public string? carac { get; set; }
        public string? resume { get; set; }
        public string? description { get; set; }
        
        public string? max { get; set; }
        public string? tests { get; set; }
        
        public int? severite { get; set; }
        public string? guerison { get; set; }
        public bool? contagieux { get; set; }
    }

    public class RootAptitude
    {
        public List<JsonAptitude> items { get; set; } = null!;
    }
}
