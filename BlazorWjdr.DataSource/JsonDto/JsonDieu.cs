namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonDieu
    {
        public int dieuid { get; set; }
        public string dieunom { get; set; } = null!;
        public string? dieudomaines { get; set; }
        public string? dieufideles { get; set; }
        public string? dieusymboles { get; set; }
        public string? dieusymbolesimg { get; set; }
        public string? dieuhistoire { get; set; }
        public string? dieucomment { get; set; }
        public int? fk_dieupatron { get; set; }
    }

    public class RootDieu
    {
        public List<JsonDieu> items { get; set; } = null!;
    }
}
