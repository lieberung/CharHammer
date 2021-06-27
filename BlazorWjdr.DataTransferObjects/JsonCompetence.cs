using System;
using System.Collections.Generic;

namespace BlazorWjdr.JsonDto
{
    public class JsonCompetence
    {
        public int competenceid { get; set; }
        public bool competencealive { get; set; }
        public DateTime competencecreatedon { get; set; }
        public DateTime competenceupdatedon { get; set; }
        public string competencelibelle { get; set; }
        public string competenceresume { get; set; }
        public string? competencedescription { get; set; }
        public bool competencebase { get; set; }
        public string competencecaract { get; set; }
        public int[]? fk_talentslies { get; set; }
        public int? fk_competencemereid { get; set; }
        public string? competencespecialis { get; set; }
        public bool competenceignore { get; set; }
    }

    public class RootCompetence
    {
        public List<JsonCompetence> items { get; set; }
    }
}
