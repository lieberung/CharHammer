using System.Collections.Generic;

namespace CharHammer.Models;

public record ScenarioDto(
    string Nom, int Note, string Difficulte, string Lien, string Duree, string Resume, 
    string DejaJoue, string[] Styles, string Source, string[] Auteurs, 
    IEnumerable<LieuDto> Lieux, IEnumerable<LieuTypeDto> LieuxTypes);