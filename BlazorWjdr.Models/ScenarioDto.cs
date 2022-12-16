namespace BlazorWjdr.Models;

public class ScenarioDto
{
    public string Nom { get; init; } = null!;
    public int Note { get; init; }
    public string Difficulte { get; init; } = null!;
    public string Lien { get; init; } = null!;
    public string Duree { get; init; } = null!;
    public string DejaJoue { get; init; } = null!;
    public string[] Styles { get; init; } = null!;
    public string Source { get; init; } = null!;
    public string[] Auteurs { get; init; } = null!;
    public LieuDto[] Lieux { get; init; } = null!;
    public LieuTypeDto[] LieuxTypes { get; init; } = null!;
    public string Resume { get; init; } = null!;
}