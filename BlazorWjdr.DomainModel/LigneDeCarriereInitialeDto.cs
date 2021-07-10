namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

    public class LigneDeCarriereInitialeDto
    {
        public int Id { get; init; }
        public int RaceId { get; init; }
        public CarriereDto Carriere { get; init; } = null!;
        public int Facteur { get; init; }
    }
}
