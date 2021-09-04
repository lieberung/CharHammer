using System.Collections.Generic;

namespace BlazorWjdr.DataSource.JsonDto
{
    public class JsonTrait
    {
        public int id { get; set; }
        public int severite { get; set; }
        public string type { get; set; } = null!;
        public string nom { get; set; } = null!;
        public string? spe { get; set; } = null;
        public string? guerison { get; set; }
        public string? description { get; set; }
        public bool? contagieux { get; set; }
        public int[]? incompatible { get; set; }

    }
    public class RootTrait
    {
        public List<JsonTrait> items { get; set; } = null!;
    }
}