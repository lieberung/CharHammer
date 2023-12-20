namespace CharHammer.Models;

public record IngredientsDto(string Prix, string Localisation, string Difficulte);

public record CreationDto(string Difficulte, string Temps);

public record PotionDto(string Reaction, string Instabilite, IngredientsDto Ingredients, CreationDto Creation);

public record EquipementDto(int Id, int? ParentId, IEnumerable<string> Groupes, string Nom,
    string Prix, string Enc, string Dispo, string Description, string? Contenance, int? Addiction,
    IEnumerable<LieuDto> Lieux, IEnumerable<LieuTypeDto> LieuxTypes, PotionDto? Potion)
{    
    public EquipementDto? Parent;
}