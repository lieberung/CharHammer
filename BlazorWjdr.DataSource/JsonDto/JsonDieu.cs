// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonDieu
    {
        public int id { get; set; }
        public int? patron_id { get; set; }
        public string nom { get; set; } = null!;
        public string domaines { get; set; } = null!;
        public string fideles { get; set; } = null!;
        public string symboles { get; set; } = null!;
        public string symboles_image { get; set; } = null!;
        public string histoire { get; set; } = null!;
        public string comment { get; set; } = null!;
    }

    public class RootDieu
    {
        public List<JsonDieu> items { get; set; } = null!;
    }
}
