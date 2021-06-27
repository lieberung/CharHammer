namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonReference
    {
        public int referenceid { get; set; }
        public string referencetitre { get; set; } = null!;
        public int? referencepublishyear { get; set; }
        public string referencecode { get; set; } = null!;
        public int referenceversion { get; set; }
    }

    public class RootReference
    {
        public List<JsonReference> items { get; set; } = null!;
    }
}
