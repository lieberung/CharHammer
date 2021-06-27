namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonRace
    {
        public int raceid { get; set; }
        public int? fk_parentid { get; set; }
        public int? fk_profilid { get; set; }
        public string racenommasculin { get; set; } = null!;
        public string? racedescription { get; set; }
        public string? racetraits { get; set; }
        public string? raceimagemale { get; set; }
        public string? raceimagefemelle { get; set; }
        public bool racepourpj { get; set; }
        public bool racepourpnj { get; set; }
        public bool racegrouponly { get; set; }
        public int[]? fk_lieux { get; set; }
        public string? racenomfeminin { get; set; }
    }

    public class RootRace
    {
        public List<JsonRace> items { get; set; } = null!;
    }

}
