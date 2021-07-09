namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonCompetence
    {
        public int id { get; set; }
        public int[]? fk_talentslies { get; set; }
        public int? fk_competencemereid { get; set; }
        public bool ignorer { get; set; }
        public bool de_base { get; set; }
        public string carac { get; set; } = null!;
        public string nom { get; set; } = null!;
        public string resume { get; set; } = null!;
        public string specialisation { get; set; } = null!;
    }

    public class RootCompetence
    {
        public List<JsonCompetence> items { get; set; } = null!;
    }
}
