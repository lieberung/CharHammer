// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
namespace BlazorWjdr.Models;

public class SortilegeDto
{
    public int Id { get; init; }
    public AptitudeDto[] Aptitudes { get; init; } = null!;
    public string Nom { get; init; } = null!;
    public string Type { get; init; } = null!;
    public string Distance { get; init; } = null!;
    public string Cible { get; init; } = null!;
    public string Duree { get; init; } = null!;
    public string? Ingredient { get; init; }
    public string Effet { get; init; } = null!;
    public int? NS { get; init; }
}