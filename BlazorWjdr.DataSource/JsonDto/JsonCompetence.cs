namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonCompetence
    {
        public int competenceid { get; set; }
        public string competencelibelle { get; set; } = null!;
        public string competenceresume { get; set; } = null!;
        public string? competencedescription { get; set; }
        public bool competencebase { get; set; }
        public string competencecaract { get; set; } = null!;
        public int[]? fk_talentslies { get; set; }
        public int? fk_competencemereid { get; set; }
        public string? competencespecialis { get; set; }
        public bool competenceignore { get; set; }
    }

    public class RootCompetence
    {
        public List<JsonCompetence> items { get; set; } = null!;
    }
}
