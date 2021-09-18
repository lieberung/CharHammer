using System.Collections.Generic;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BlazorWjdr.DataSource.JsonDto
{
    public class JsonEquipement
    {
        public int id { get; set; }
        public string[]? groupes { get; set; }
        public string nom { get; set; } = null!;
        public string prix { get; set; } = null!;
        public string enc { get; set; } = null!;
        public string dispo { get; set; } = null!;
        public string description { get; set; } = null!;
    }

    public class RootEquipement
    {
        public List<JsonEquipement> items { get; set; } = null!;
    }
}