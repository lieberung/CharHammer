// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonCompetence
    {
        public int id { get; set; }
        public int[]? talents { get; set; }
        public int[]? traits { get; set; }
        public int? parent { get; set; }
        public bool ignorer { get; set; }
        public bool de_base { get; set; }
        public string carac { get; set; } = null!;
        public string nom { get; set; } = null!;
        public string? nom_en { get; set; }
        public string resume { get; set; } = null!;
        public string? resume_v2 { get; set; }
        public string? spe { get; set; }
    }

    public class RootCompetence
    {
        public List<JsonCompetence> items { get; set; } = null!;
    }
}
