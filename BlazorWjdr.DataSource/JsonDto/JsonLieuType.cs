// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonLieuType
    {
        public int id { get; set; }
        public int? parentid { get; set; }
        public string libelle { get; set; } = null!;
    }

    public class RootLieuType
    {
        public List<JsonLieuType> items { get; set; } = null!;
    }
}
