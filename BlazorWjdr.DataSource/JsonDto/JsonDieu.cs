// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonAptitudesAssociees
    {
        public int[]? initie { get; set; }
        public int[]? pretre_sans_ordre { get; set; }
    }

    public class JsonRegleAssociee
    {
        public string titre { get; set; }
        public string description { get; set; }
    }

    public class JsonSecte
    {
        public string nom { get; set; }
        public string description { get; set; }
    }

    public class JsonTemple
    {
        public string nom { get; set; }
        public string description { get; set; }
    }

    public class JsonPersonnalite
    {
        public string nom { get; set; }
        public string description { get; set; }
    }

    public class JsonDieu
    {
        public int id { get; set; }
        public int? patron { get; set; } = null;
        public string nom { get; set; } = null!;
        
        public string? domaines { get; set; }
        public string? fideles { get; set; }
        public string? histoire { get; set; }
        public string? comment { get; set; }
        
        public string? symboles { get; set; }
        public int? siege { get; set; }
        public JsonAptitudesAssociees? aptitudes { get; set; }
        public string? chef { get; set; }
        public string? fetes { get; set; }
        public string? livres { get; set; }
        public string? intro { get; set; }
        public string? penitences { get; set; }
        public string? culte { get; set; }
        public JsonRegleAssociee[]? regles { get; set; }
        public string? dogme { get; set; }
        public string? initiation { get; set; }
        public string? pretrise { get; set; }
        public string[]? commandements { get; set; }
        public string? cultistes { get; set; }
        public string? structure { get; set; }
        public JsonSecte[]? sectes { get; set; }
        public string[]? ambiance { get; set; }
        public JsonTemple[]? temples { get; set; }
        public JsonPersonnalite[]? personnalites { get; set; }
    }

    public class RootDieu
    {
        public List<JsonDieu> items { get; set; } = null!;
    }
}
