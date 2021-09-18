namespace BlazorWjdr.Models
{
    public class ArmureDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Pa { get; set; } = null!;
        public string Zones { get; set; } = null!;
        public string Prix { get; set; } = null!;
        public string Enc { get; set; } = null!;
        public string Disponibilite { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}