namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonArme
    {
        public int id { get; set; }
        public List<int> talents { get; set; } = null!;
        public List<int> groupes { get; set; } = null!;
        public List<int> attributs { get; set; } = null!;
        public string nom { get; set; } = null!;
        public string degats { get; set; } = null!;
        public string degats_v2 { get; set; } = null!;
        public string? allonge { get; set; }
        public string? portee { get; set; }
        public string? rechargement { get; set; }
        public string enc { get; set; } = null!;
        public string prix { get; set; } = null!;
        public string dispo { get; set; } = null!;
        public string description { get; set; } = null!;
    }

    public class RootArme
    {
        public List<JsonArme> items { get; set; } = null!;
    }

}
