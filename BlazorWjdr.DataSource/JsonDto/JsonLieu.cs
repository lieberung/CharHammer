namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonLieu
    {
        public int id { get; set; }
        public int fk_typeid { get; set; }
        public int? fk_parentid { get; set; }
        public string nom { get; set; } = null!;
        public string? description { get; set; }
        public string? image { get; set; }
    }

    public class RootLieu
    {
        public List<JsonLieu> items { get; set; } = null!;
    }
}
