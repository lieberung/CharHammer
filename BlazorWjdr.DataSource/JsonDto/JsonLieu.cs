namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonLieu
    {
        public int lieuid { get; set; }
        public int fk_typeid { get; set; }
        public int fk_parentid { get; set; }
        public string lieulibelle { get; set; } = null!;
        public string? lieudescription { get; set; }
        public string? lieuimage { get; set; }
    }

    public class RootLieu
    {
        public List<JsonLieu> items { get; set; } = null!;
    }
}
