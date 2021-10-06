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

    public class JsonLieu
    {
        public int id { get; set; }
        public int fk_typeid { get; set; }
        public int? fk_parentid { get; set; }
        public string nom { get; set; } = null!;
        public string? description { get; set; }
    }

    public class RootLieu
    {
        public List<JsonLieuType> types { get; set; } = null!;
        public List<JsonLieu> items { get; set; } = null!;
    }
}
