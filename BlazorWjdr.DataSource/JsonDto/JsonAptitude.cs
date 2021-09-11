// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

using Newtonsoft.Json;

namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonAptitude
    {
        public int id { get; set; }
        [JsonIgnore]
        public int id_old { get; set; }
        public int? parent { get; set; }

        public string categ { get; set; } = null!; // skill / talent / trait
        public string categ_spe { get; set; } = null!;
        public string nom { get; set; } = null!;
        public string? nom_en { get; set; }
        public string? spe { get; set; }

        public List<int> aptitudes { get; set; } = new();
        public List<int> incompatibles { get; init; } = null!;

        [JsonIgnore]
        public int[]? skills { get; set; }
        [JsonIgnore]
        public int[]? talents { get; set; }
        [JsonIgnore]
        public int[]? traits { get; set; }

        public bool ignorer { get; set; }
        public string? carac { get; set; }
        public string resume { get; set; } = null!;
        public string description { get; set; } = null!;
        public string? resume_v2 { get; set; }
        public string? description_v2 { get; set; }
        
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
