namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonDomaine
    {
        public int id { get; set; }
        public string nom { get; set; } = null!;
        public string? comment { get; set; }
    }

    public class RootDomaine
    {
        public List<JsonDomaine> items { get; set; } = null!;
    }
}
