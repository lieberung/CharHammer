namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonTableCarriereInitiale
    {
        public int race { get; set; }
        public int carriere { get; set; }
        public int facteur { get; set; }
    }

    public class RootTableCarriereInitiale
    {
        public List<JsonTableCarriereInitiale> items { get; set; } = null!;
    }
}
