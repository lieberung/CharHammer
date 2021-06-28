namespace BlazorWjdr.DataSource.JsonDto
{
    using System;
    using System.Collections.Generic;

    public class JsonPj
    {
        public int id { get; set; }
        public DateTime? date_creation { get; set; }
        public string nom_joueur { get; set; }
        public int xp_actuel { get; set; }
        public int xp_total { get; set; }
        public int fk_profilinitialid { get; set; }
    }

    public class RootPj
    {
        public List<JsonPj> items { get; set; }
    }
}
