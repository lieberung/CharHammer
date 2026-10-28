namespace CharHammer.Models;

public class ArmeDto(
    int Id, 
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
    public int Id { get; } = Id;
    public IReadOnlyList<AptitudeDto> CompetencesDeMaitrise { get; } = CompetencesDeMaitrise;
    public IReadOnlyList<ArmeAttributDto> Attributs { get; } = Attributs;
    public IReadOnlyList<ArmeAttributDto> Groupes { get; } = Groupes;
    public string Nom { get; } = Nom;
    public string Degats { get; } = Degats;
    public string Allonge { get; } = Allonge;
    public string Portee { get; } = Portee;
    public string Rechargement { get; } = Rechargement;
    public string Encombrement { get; } = Encombrement;
    public string Prix { get; } = Prix;
    public string Disponibilite { get; } = Disponibilite;
    public string Description { get; } = Description;

    public bool EstUneArmeDeCaC => Allonge != "";
    public bool EstUneArmeDeTir => Portee != "" && !EstUneMunition;
    public bool EstUneMunition => Groupes.Any(g => g.Nom == "Munitions");
}
