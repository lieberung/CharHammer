namespace CharHammer.Models;

public record ScenarioDto(
    string Nom, int Note, string Difficulte, string Lien, string Duree, string Resume, 
    string DejaJoue, string[] Styles, string Source, string[] Auteurs, 
    IReadOnlyList<LieuDto> Lieux, IReadOnlyList<LieuTypeDto> LieuxTypes);