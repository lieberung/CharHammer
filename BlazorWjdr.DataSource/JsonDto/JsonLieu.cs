// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
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
        public int type { get; set; }
        public int? parent { get; set; }
        public string nom { get; set; } = null!;
        public string? population { get; set; }
        public string? allegeance { get; set; }
        public string? industrie { get; set; }
        public string? description { get; set; }
        public bool ignorer { get; set; }
    }

    public class RootLieu
    {
        public List<JsonLieuType> types { get; set; } = null!;
        public List<JsonLieu> items { get; set; } = null!;
    }
}
