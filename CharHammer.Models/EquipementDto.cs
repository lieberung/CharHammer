namespace CharHammer.Models;

public record IngredientsDto(string Prix, string Localisation, string Difficulte);

public record CreationDto(string Difficulte, string Temps);

public record PotionDto(string Reaction, string Instabilite, IngredientsDto Ingredients, CreationDto Creation);

public record EquipementDto(int Id, int? ParentId, IReadOnlyList<string> Groupes, string Nom,
    string Prix, string Enc, string Dispo, string Description, string? Contenance, int? Addiction,
    IReadOnlyList<LieuDto> Lieux, IReadOnlyList<LieuTypeDto> LieuxTypes, PotionDto? Potion)
{    
    public EquipementDto? Parent;
}