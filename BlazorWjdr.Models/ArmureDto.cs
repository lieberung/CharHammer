// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace BlazorWjdr.Models;

public class ArmureDto
{
    public int Id { get; set; }
    public int? ParentId { get; init; }
    public ArmureDto? Parent { get; set; }
    public string Nom { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Pa { get; set; } = null!;
    public string Zones { get; set; } = null!;
    public string Prix { get; set; } = null!;
    public string Enc { get; set; } = null!;
    public string Disponibilite { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ArmeAttributDto[] Attributs { get; init; } = null!;

    public bool TypeLegere => Type == "Cuir";
    public bool TypeMoyenne => Type == "Mailles";
    public bool TypeLourde => Type == "Plaques";
    public bool TypeVetement => Type == "Vêtement";
}