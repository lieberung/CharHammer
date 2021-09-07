// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonReference
    {
        public int id { get; set; }
        public string titre { get; set; } = null!;
        public int? publishyear { get; set; }
        public string code { get; set; } = null!;
        public int version { get; set; }
    }

    public class RootReference
    {
        public List<JsonReference> items { get; set; } = null!;
    }
}
