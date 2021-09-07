// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonPersonnage
    {
        public int id { get; set; }
        public int? fk_carrierepereid { get; set; }
        public int? fk_carrieremereid { get; set; }
        public int? fk_signeastralid { get; set; }
        public string? freres_et_soeurs { get; set; }
        public int main_directrice { get; set; }
        public bool mort { get; set; }
        public int[]? fk_cheminprofess { get; set; }
        public string? cheveux { get; set; }
        public string? yeux { get; set; }
    }

    public class RootPersonnage
    {
        public List<JsonPersonnage> items { get; set; } = null!;
    }
}
