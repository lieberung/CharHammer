// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    public class JsonScenario
    {
        public string nom { get; set; } = null!;
        public int note { get; set; }
        public string? lien { get; set; }
        public string? duree { get; set; }
        public string? deja_joue { get; set; }
        public string? resume { get; set; }
        public string[]? styles { get; set; }
        public int[]? lieux { get; set; }
        public string? source { get; set; }
        public string? commentaire { get; set; }
        public int[]? lieuxtypes { get; set; }
        public string? difficulte { get; set; }
    }
    
    public class RootScenario
    {
        public JsonScenario[] items { get; set; } = null!;
    }
}