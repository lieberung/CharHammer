namespace BlazorWjdr.DomainModel
{
    public class ReferenceDto
    {
        public int Id { get; set; }
        public string Titre { get; set; } = null!;
        public int? AnneeDePublication { get; set; }
        public string Code { get; set; } = null!;
        public int Version { get; set; }
    }
}
