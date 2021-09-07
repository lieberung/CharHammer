// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonTable
    {
        public int id { get; set; }
        public string titre { get; set; } = null!;
        public string description { get; set; } = null!;
        public string[]? styles_th { get; set; }
        public string[]? styles_td { get; set; }
        public string[][] lignes { get; set; } = null!;
    }

    public class RootTable
    {
        public List<JsonTable> items { get; set; } = null!;
    }
}