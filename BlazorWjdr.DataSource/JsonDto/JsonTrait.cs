using System.Collections.Generic;

namespace BlazorWjdr.DataSource.JsonDto
{
    public class JsonTrait
    {
        public int id { get; set; }
        public int severite { get; set; }
        public string type { get; set; }
        public string nom { get; set; }
        public string guerison { get; set; }
        public string description { get; set; }
        public bool? contagieux { get; set; }

    }
    public class RootTrait
    {
        public List<JsonTrait> items { get; set; }
    }
}