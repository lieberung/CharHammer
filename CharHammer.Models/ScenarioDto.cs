namespace CharHammer.Models;

public class ScenarioDto(
    string Nom, int Note, string Difficulte, string Lien, string Duree, string Resume, 
    string DejaJoue, string[] Styles, string Source, string[] Auteurs, 
    IReadOnlyList<LieuDto> Lieux, IReadOnlyList<LieuTypeDto> LieuxTypes)
{
    public string Nom { get; } = Nom;
    public int Note { get; } = Note;
    public string Difficulte { get; } = Difficulte;
    public string Lien { get; } = Lien;
    public string Duree { get; } = Duree;
    public string Resume { get; } = Resume;
    public string DejaJoue { get; } = DejaJoue;
    public string[] Styles { get; } = Styles;
    public string Source { get; } = Source;
    public string[] Auteurs { get; } = Auteurs;
    public IReadOnlyList<LieuDto> Lieux { get; } = Lieux;
    public IReadOnlyList<LieuTypeDto> LieuxTypes { get; } = LieuxTypes;
}