namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonGrave
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string epitaph { get; set; } = null!;
        public string last_words { get; set; } = null!;
        public string land_lord { get; set; } = null!;
    }

    public class RootGrave
    {
        public List<JsonGrave> items { get; set; } = null!;
    }
}
