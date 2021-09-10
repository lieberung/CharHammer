// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonCreature
    {
        public int id { get; set; }
        public int profil_actuel { get; set; }
        public int user { get; set; }
        public int race { get; set; }
        public string nom { get; set; } = null!;
        public int? sexe { get; set; }
        public string? histoire { get; set; }
        public string? description { get; set; }
        public int[]? competences { get; set; }
        public int[]? talents { get; set; }
        public int[]? origines { get; set; }
        public int[] membrede { get; set; } = null!;
        public int? poids { get; set; }
        public int? taille { get; set; }
        public int? age { get; set; }
        public string? psycho { get; set; }
        public int[]? traits { get; set; }
        
        public System.DateTime? date_creation { get; set; }
        public string? nom_joueur { get; set; } = null!;
        public int xp_actuel { get; set; }
        public int xp_total { get; set; }
        public int? fk_profilinitialid { get; set; }
        
        public int? fk_carrierepereid { get; set; }
        public int? fk_carrieremereid { get; set; }
        public int? fk_signeastralid { get; set; }
        public string? freres_et_soeurs { get; set; }
        public int? main_directrice { get; set; }
        public bool mort { get; set; }
        public int[]? fk_cheminprofess { get; set; }
        public string? cheveux { get; set; }
        public string? yeux { get; set; }
    }

    public class RootCreature
    {
        public List<JsonCreature> items { get; set; } = null!;
    }
}
