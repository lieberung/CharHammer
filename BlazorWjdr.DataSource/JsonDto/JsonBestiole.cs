// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonBestiole
    {
        public int id { get; set; }
        public int profil_actuel { get; set; }
        public int user { get; set; }
        public string nom { get; set; } = null!;
        public string histoire { get; set; } = null!;
        public string comment { get; set; } = "";
        public int[]? competences { get; set; }
        public int[]? talents { get; set; }
        public int[]? origines { get; set; }
        public int race { get; set; }
        public int[] membrede { get; set; } = null!;
        public int? poids { get; set; }
        public int? taille { get; set; }
        public int? age { get; set; }
        public int sexe { get; set; }
        public string psycho { get; set; } = null!;
        public int[]? traits { get; set; }
    }

    public class RootBestiole
    {
        public List<JsonBestiole> items { get; set; } = null!;
    }
}
