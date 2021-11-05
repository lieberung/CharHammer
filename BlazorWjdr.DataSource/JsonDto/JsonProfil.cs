// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonProfil
    {
        public int cc { get; set; }
        public int ct { get; set; }
        public int f { get; set; }
        public int e { get; set; }
        public int i { get; set; }
        public int ag { get; set; }
        public int dex { get; set; }
        public int intel { get; set; }
        public int fm { get; set; }
        public int soc { get; set; }
        public int m { get; set; }
    }

    public class RootProfil
    {
        public List<JsonProfil> items { get; set; } = null!;
    }
}
