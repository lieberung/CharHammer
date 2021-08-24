namespace BlazorWjdr.Models
{
    public class DieuDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
        public string Domaines { get; init; } = null!;
        public string Fideles { get; init; } = null!;
        public string Symboles { get; init; } = null!;
        public string SymbolesImages { get; set; } = null!;
        public string Histoire { get; init; } = null!;
        public string Commentaire { get; init; } = null!;

        public int? PatronId { get; init; }
        public DieuDto? Patron { get; set; }
    }
}
