// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonCreature
    {
        public int id { get; set; }
        public JsonProfil profil_actuel { get; set; } = null!;
        public int? user { get; set; }
        public int race { get; set; }
        public string nom { get; set; } = null!;
        public int? sexe { get; set; }
        public int[]? origines { get; set; }
        public string[]? membrede { get; set; } = null!;
        public int? poids { get; set; }
        public int? taille { get; set; }
        public int? age { get; set; }
        
        public int[]? aptitudes { get; set; }
        public int[]? aptitudes_facultatives { get; set; }
        
        public string? description { get; set; }
        public string? psycho { get; set; }
        public string? histoire { get; set; }
        public string? ambitions { get; set; }
        public string? notes { get; set; }

        public string? date_creation { get; set; }
        public int? xp_actuel { get; set; }
        public int? xp_total { get; set; }
        public JsonProfil? profil_initial { get; set; }
        
        public int[]? cheminement { get; set; }
        public int? carriere_du_pere { get; set; }
        public int? carriere_de_la_mere { get; set; }
        public int? fk_signeastralid { get; set; }
        public string? freres_et_soeurs { get; set; }
        public int? main_directrice { get; set; }
        public bool mort { get; set; }
        public string? cheveux { get; set; }
        public string? yeux { get; set; }

        public int[]? armes { get; set; }
        public int[]? armures { get; set; }
        public int[]? equipement { get; set; }
    }

    public class RootCreature
    {
        public List<JsonCreature> items { get; set; } = null!;
    }
}
