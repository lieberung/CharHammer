namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonChoixCompetence
    {
        public int id { get; set; }
        public int[] choixcompetencekeys { get; set; } = null!;
    }

    public class RootChoixCompetence
    {
        public List<JsonChoixCompetence> items { get; set; } = null!;
    }
}
