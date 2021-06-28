namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;
    
    public class JsonTableCalcPoints
    {
        public int id { get; set; }
        public int fk_raceid { get; set; }
        public int dicevalue { get; set; }
        public int blessures { get; set; }
        public int destin { get; set; }
        public int siblings { get; set; }
        public string cheveux { get; set; } = null!;
        public string yeux { get; set; } = null!;
    }

    public class RootTableCalcPoints
    {
        public List<JsonTableCalcPoints> items { get; set; } = null!;
    }
}
