namespace CharHammer.Models;

public class SortilegeDto(
    int Id, IReadOnlyList<AptitudeDto> Aptitudes, string Nom, string Type, string Distance,
    string Cible, string Duree, string? Ingredient, string Effet, int? NS)
{
    public int Id { get; } = Id;
    public IReadOnlyList<AptitudeDto> Aptitudes { get; } = Aptitudes;
    public string Nom { get; } = Nom;
    public string Type { get; } = Type;
    public string Distance { get; } = Distance;
    public string Cible { get; } = Cible;
    public string Duree { get; } = Duree;
    public string? Ingredient { get; } = Ingredient;
    public string Effet { get; } = Effet;
    public int? NS { get; } = NS;
}