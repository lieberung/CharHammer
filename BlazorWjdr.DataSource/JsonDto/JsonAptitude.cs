// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

using System;

namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonAptitude
    {
        public int id { get; set; }
        public int id_old { get; set; }
        public int? parent { get; set; }

        public string categ { get; set; } = null!; // skill / talent / trait
        public string categ_spe { get; set; } = null!;
        public string nom { get; set; } = null!;
        public string? spe { get; set; }

        public int[] aptitudes { get; set; } = Array.Empty<int>();
        public int[]? incompatibles { get; set; }

        public int[]? skills { get; set; }
        public int[]? talents { get; set; }
        public int[]? traits { get; set; }

        public bool ignorer { get; set; }
        public string? carac { get; set; }
        public string resume { get; set; } = null!;
        public string description { get; set; } = null!;
        
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
