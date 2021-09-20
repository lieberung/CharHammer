using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace BlazorWjdr.DataSource.JsonDto
{
    public class JsonArmure
    {
        public int id { get; set; }
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

    public class RootArmure
    {
        public List<JsonArmure> items { get; set; } = null!;
    }
}