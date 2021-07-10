namespace BlazorWjdr.Models
{
    public class LigneDeCarriereInitialeDto
    {
        public RaceDto Race { get; init; } = null!;
        public CarriereDto Carriere { get; init; } = null!;
        public int Facteur { get; init; }
    }
}
