namespace BlazorWjdr.Models
{
    public class ReferenceDto
    {
        public int Id { get; init; }
        public string Titre { get; init; } = null!;
        public int? AnneeDePublication { get; init; }
        public string Code { get; init; } = null!;
        public int Version { get; init; }

        public string Image => $"{Id}.jpg";
    }
}
