// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonAptitudesAssociees
    {
        public List<int> initie { get; set; }
        public List<int> pretre_sans_ordre { get; set; }
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
        public int? patron { get; set; }
        public string nom { get; set; }
        public string domaines { get; set; }
        public string fideles { get; set; }
        public string symboles { get; set; }
        public string symboles_image { get; set; }
        public string histoire { get; set; }
        public string comment { get; set; }
        public int? siege { get; set; }
        public JsonAptitudesAssociees JsonAptitudesAssociees { get; set; }
        public string chef { get; set; }
        public string fetes { get; set; }
        public string livres { get; set; }
        public string intro { get; set; }
        public string culte { get; set; }
        public List<JsonRegleAssociee> regles { get; set; }
        public string dogme { get; set; }
        public string initiation { get; set; }
        public string pretrise { get; set; }
        public List<string> commandements { get; set; }
        public string cultistes { get; set; }
        public string structure { get; set; }
        public List<JsonSecte> sectes { get; set; }
        public List<string> ambiance { get; set; }
        public List<JsonTemple> temples { get; set; }
        public List<JsonPersonnalite> personnalites { get; set; }
    }

    public class RootDieu
    {
        public List<JsonDieu> items { get; set; } = null!;
    }
}
