namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonChoixTalent
    {
        public int id { get; set; }
        public int[] choixtalentkeys { get; set; } = null!;
    }

    public class RootChoixTalent
    {
        public List<JsonChoixTalent> items { get; set; } = null!;
    }
}