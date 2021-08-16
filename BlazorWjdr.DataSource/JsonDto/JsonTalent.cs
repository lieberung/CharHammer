namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonTalent
    {
        public int id { get; set; }
        public int? parent_id { get; set; }
        public bool ignorer { get; set; }
        public bool trait { get; set; }
        public string nom { get; set; } = null!;
        public string resume { get; set; } = null!;
        public string description { get; set; } = null!;
        public string? spe { get; set; }
    }

    public class RootTalent
    {
        public List<JsonTalent> items { get; set; } = null!;
    }

}
