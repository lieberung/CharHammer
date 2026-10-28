namespace CharHammer.Models;

public record IngredientsDto(string Prix, string Localisation, string Difficulte);

public record CreationDto(string Difficulte, string Temps);

public record PotionDto(string Reaction, string Instabilite, IngredientsDto Ingredients, CreationDto Creation);

public class EquipementDto(int Id, int? ParentId, IReadOnlyList<string> Groupes, string Nom,
    string Prix, string Enc, string Dispo, string Description, string? Contenance, int? Addiction,
    IReadOnlyList<LieuDto> Lieux, IReadOnlyList<LieuTypeDto> LieuxTypes, PotionDto? Potion)
{
    public int Id { get; } = Id;
    public int? ParentId { get; } = ParentId;
    public IReadOnlyList<string> Groupes { get; } = Groupes;
    public string Nom { get; } = Nom;
    public string Prix { get; } = Prix;
    public string Enc { get; } = Enc;
    public string Dispo { get; } = Dispo;
    public string Description { get; } = Description;
    public string? Contenance { get; } = Contenance;
    public int? Addiction { get; } = Addiction;
    public IReadOnlyList<LieuDto> Lieux { get; } = Lieux;
    public IReadOnlyList<LieuTypeDto> LieuxTypes { get; } = LieuxTypes;
    public PotionDto? Potion { get; } = Potion;

    
    public EquipementDto? Parent;
}