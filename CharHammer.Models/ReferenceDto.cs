namespace CharHammer.Models;

public class ReferenceDto(int Id, string Titre, int AnneeDePublication, int Version)
{
    public int Id { get; } = Id;
    public string Titre { get; } = Titre;
    public int AnneeDePublication { get; } = AnneeDePublication;
    public int Version { get; } = Version;

    public string Image => $"images/books/{Id}.jpg";
}
