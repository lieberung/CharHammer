using System.Collections.Generic;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BlazorWjdr.DataSource.JsonDto
{
    public class JsonIngredients
    {
        public string prix { get; set; } = null!;
        public string localisation { get; set; } = null!;
        public string difficulte { get; set; } = null!;
    }

    public class JsonCreation
    {
        public string difficulte { get; set; } = null!;
        public string temps { get; set; } = null!;
    }

    public class JsonPotion
    {
        public string reaction { get; set; } = null!;
        public string instabilite { get; set; } = null!;
        public JsonIngredients ingredients { get; set; } = null!;
        public JsonCreation creation { get; set; } = null!;
    }
    
    public class JsonEquipement
    {
        public int id { get; set; }
        public int? parent { get; set; }
        public string[]? groupes { get; set; }
        public string nom { get; set; } = null!;
        public string prix { get; set; } = null!;
        public string? enc { get; set; }
        public string dispo { get; set; } = null!;
        public string description { get; set; } = null!;
        
        public string? contenance { get; set; }
        public int? addiction { get; set; }
        public int[]? lieux { get; set; }
        public int[]? lieuxtypes { get; set; }

        public JsonPotion? potion { get; set; }
    }

    public class RootEquipement
    {
        public List<JsonEquipement> items { get; set; } = null!;
    }
}