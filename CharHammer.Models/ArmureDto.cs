namespace CharHammer.Models;

public class ArmureDto(
    int Id,
    string Nom,
    string Type,
    string Pa,
    string Zones,
    string Prix,
    string Enc,
    string Disponibilite,
    string Description,
    IReadOnlyList<ArmeAttributDto> Attributs)
{
    public int Id { get; } = Id;
    public string Nom { get; } = Nom;
    public string Type { get; } = Type;
    public string Pa { get; } = Pa;
    public string Zones { get; } = Zones;
    public string Prix { get; } = Prix;
    public string Enc { get; } = Enc;
    public string Disponibilite { get; } = Disponibilite;
    public string Description { get; } = Description;
    public IReadOnlyList<ArmeAttributDto> Attributs { get; } = Attributs;

    public bool TypeLegere => Type == "Cuir";
    public bool TypeMoyenne => Type == "Mailles";
    public bool TypeLourde => Type == "Plaques";
    public bool TypeVetement => Type == "Vêtement";
}