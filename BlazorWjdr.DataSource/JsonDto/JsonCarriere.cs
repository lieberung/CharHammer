// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonCarriere
    {
        public int id { get; set; }
        public int? parent { get; set; }
        public string nom { get; set; } = null!;
        public bool avancee { get; set; }
        public int plan { get; set; }
        public int[]? avancements { get; set; }
        public int[]? debouch { get; set; }
        public int[]? aptitudes { get; set; }
        public int[][]? aptitudes_choix { get; set; }
        public string? groupe { get; set; }
        public int? source_livre { get; set; }
        public string? source_page { get; set; }
        public string description { get; set; } = null!;
        public string[]? ambiance { get; set; }
        public string? restriction { get; set; }
        public string dotations { get; set; } = null!;
    }
    
    public class RootCarriere
    {
        public List<JsonCarriere> items { get; set; } = null!;
    }
}
