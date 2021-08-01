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
        public int fk_plandecarriereid { get; set; }
        public string? source { get; set; }
        public int[]? fk_debouches { get; set; }
        public int[]? fk_competences { get; set; }
        public int[]? fk_talents { get; set; }
        public int[]? fk_choixcompetences { get; set; }
        public int[]? fk_choixtalents { get; set; }
        public int? fk_sourceid { get; set; }
        public string dotations { get; set; } = null!;
        public int? fk_parentcarriereid { get; set; }
    }

    public class RootCarriere
    {
        public List<JsonCarriere> items { get; set; } = null!;
    }
}
