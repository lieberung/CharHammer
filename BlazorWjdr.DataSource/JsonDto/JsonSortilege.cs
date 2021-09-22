// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class JsonSortilege
    {
        public int id { get; set; }
        public int[]? aptitudes { get; set; }
        public string nom { get; set; } = null!;
        public string type { get; set; } = null!;
        public string distance { get; set; } = null!;
        public string cible { get; set; } = null!;
        public string duree { get; set; } = null!;
        public string effet { get; set; } = null!;
        public int? ns { get; set; }
    }

    public class RootSortilege
    {
        public JsonSortilege[] items { get; set; } = null!;
    }
}