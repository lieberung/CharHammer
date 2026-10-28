namespace CharHammer.Models;

public record ArmeDto(
    int Id, 
    int? ParentId, 
    IReadOnlyList<AptitudeDto> CompetencesDeMaitrise, 
    IReadOnlyList<ArmeAttributDto> Attributs, 
    IReadOnlyList<ArmeAttributDto> Groupes, 
    string Nom, 
    string Degats, 
    string Allonge, 
    string Portee, 
    string Rechargement,
    string Encombrement, 
    string Prix, 
    string Disponibilite, 
    string Description)
{
    //public ArmeDto? Parent;
    public bool EstUneArmeDeCaC => Allonge != "";
    public bool EstUneArmeDeTir => Portee != "" && !EstUneMunition;
    public bool EstUneMunition => Groupes.Any(g => g.Nom == "Munitions");
}
