namespace BlazorWjdr.DataSource.JsonDto
{
    using System;
    using System.Collections.Generic;

    public class JsonSeance
    {
        public int id { get; set; }
        public int fk_teamid { get; set; }
        public int fk_campainid { get; set; }
        public DateTime date { get; set; }
        public int duree { get; set; }
        public string titre { get; set; } = null!;
        public int xp { get; set; }
        public string xp_comment { get; set; } = null!;
        public string resume { get; set; } = null!;
        public int acte { get; set; }
    }

    public class RootSeance
    {
        public List<JsonSeance> items { get; set; } = null!;
    }
}
