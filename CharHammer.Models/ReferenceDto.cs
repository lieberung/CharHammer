namespace CharHammer.Models;

public record ReferenceDto(int Id, string Titre, int AnneeDePublication, int Version)
{
    public string Image => $"/images/books/{Id}.jpg";
}
