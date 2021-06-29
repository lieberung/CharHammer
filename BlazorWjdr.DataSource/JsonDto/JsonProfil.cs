namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonProfil
    {
        public int id { get; set; }
        public int cc { get; set; }
        public int ct { get; set; }
        public int f { get; set; }
        public int e { get; set; }
        public int ag { get; set; }
        public int intel { get; set; }
        public int fm { get; set; }
        public int soc { get; set; }
        public int a { get; set; }
        public int b { get; set; }
        public int bf { get; set; }
        public int be { get; set; }
        public int m { get; set; }
        public int mag { get; set; }
        public int pf { get; set; }
        public int pd { get; set; }
    }

    public class RootProfil
    {
        public List<JsonProfil> items { get; set; } = null!;
    }
}
