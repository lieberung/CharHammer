using System.Collections.Generic;

namespace CharHammer.Models;

public record ArmureDto(
    int Id,
    int? ParentId,
    string Nom,
    string Type,
    string Pa,
    string Zones,
    string Prix,
    string Enc,
    string Disponibilite,
    string Description,
    IEnumerable<ArmeAttributDto> Attributs)
{
    //public ArmureDto? Parent { get; set; }
    public bool TypeLegere => Type == "Cuir";
    public bool TypeMoyenne => Type == "Mailles";
    public bool TypeLourde => Type == "Plaques";
    public bool TypeVetement => Type == "Vêtement";
}