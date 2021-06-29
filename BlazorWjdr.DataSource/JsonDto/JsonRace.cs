namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonRace
    {
        public int id { get; set; }
        public int? parent_id { get; set; }
        public int? profil_id { get; set; }
        public List<int>? lieux_ids { get; set; }
        public bool pour_pj { get; set; }
        public bool pour_pnj { get; set; }
        public bool group_only { get; set; }
        public string nom_masculin { get; set; } = null!;
        public string nom_feminin { get; set; } = null!;
        public string description { get; set; } = null!;
        public string traits { get; set; } = null!;
        public string image_male { get; set; } = null!;
        public string image_femelle { get; set; } = null!;
    }

    public class RootRace
    {
        public List<JsonRace> items { get; set; } = null!;
    }

}
