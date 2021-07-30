namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonBestiole
    {
        public int id { get; set; }
        public int fk_profilactuelid { get; set; }
        public int fk_userid { get; set; }
        public string nom { get; set; } = null!;
        public string histoire { get; set; } = null!;
        public string comment { get; set; } = "";
        public int[]? fk_competences { get; set; }
        public int[]? fk_talents { get; set; }
        public int[]? fk_origines { get; set; }
        public int fk_raceid { get; set; }
        public int[] membrede { get; set; } = null!;
        public int? poids { get; set; }
        public int? taille { get; set; }
        public int? age { get; set; }
        public int sexe { get; set; }
        public string psycho { get; set; } = null!;
    }

    public class RootBestiole
    {
        public List<JsonBestiole> items { get; set; } = null!;
    }
}
