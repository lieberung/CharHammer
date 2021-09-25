// ReSharper disable InconsistentNaming
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace BlazorWjdr.Models
{
    public class EquipementDto
    {
        public int Id { get; set; }
        public string[] Groupes { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prix { get; set; } = null!;
        public string Enc { get; set; } = null!;
        public string Dispo { get; set; } = null!;
        public string Description { get; set; } = null!;
        
        public string? Contenance { get; init; }
        public int? Addiction { get; init; }
        public LieuDto[] Lieux { get; init; }
    }
}