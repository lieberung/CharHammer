namespace CharHammer.Models;

public class ArmeAttributDto(int Id, string Type, string Nom, string Description)
{
    public int Id { get; } = Id;
    public string Type { get; } = Type;
    public string Nom { get; } = Nom;
    public string Description { get; } = Description;
}
