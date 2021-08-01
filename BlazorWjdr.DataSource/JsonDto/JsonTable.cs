namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonTable
    {
        public int id { get; set; }
        public string titre { get; set; }
        public string description { get; set; }
        public string[][] lignes { get; set; }
    }

    public class RootTable
    {
        public List<JsonTable> items { get; set; }
    }
}