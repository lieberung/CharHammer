namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonTableCarriereInitiale
    {
        public int id { get; set; }
        public int fk_raceid { get; set; }
        public int fk_carriereid { get; set; }
        public int facteur { get; set; }
    }

    public class RootTableCarriereInitiale
    {
        public List<JsonTableCarriereInitiale> items { get; set; } = null!;
    }
}
