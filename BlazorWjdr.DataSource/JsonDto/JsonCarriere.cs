namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonCarriere
    {
        public int id { get; set; }
        public string? groupe { get; set; }
        public string nom { get; set; } = null!;
        public string description { get; set; } = null!;
        public bool avancee { get; set; }
        public string? restriction { get; set; }
        public int plan { get; set; }
        public string? source { get; set; }
        public int[]? debouch { get; set; }
        public int[]? competences { get; set; }
        public int[]? talents { get; set; }
        public int[]? competenceschoix { get; set; }
        public int[]? talentschoix { get; set; }
        public int? fk_sourceid { get; set; }
        public string dotations { get; set; } = null!;
        public int? parent { get; set; }
    }

    public class RootCarriere
    {
        public List<JsonCarriere> items { get; set; } = null!;
    }
}
