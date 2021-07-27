namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonArme
    {
        public int id { get; set; }
        public List<int> talents { get; set; }
        public string groupe { get; set; }
        public List<int> attributs { get; set; }
        public string nom { get; set; }
        public string degats { get; set; }
        public string portee { get; set; }
        public string rechargement { get; set; }
        public string enc { get; set; }
        public string prix { get; set; }
        public string disponibilite { get; set; }
        public string description { get; set; }
    }

    public class RootArme
    {
        public List<JsonArme> items { get; set; }
    }

}
