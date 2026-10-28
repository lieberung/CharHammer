namespace CharHammer.Models;

public record DieuDto(int Id, string Nom, string Domaines, string Fideles, string Symboles, string Histoire, 
    string Commentaire, int? PatronId, LieuDto? Siege, string Chef, string Fetes, string LivresSaints, 
    string Intro, string Penitences, string Culte, string Dogme, string Initiation, string Pretrise, 
    string Cultistes, string Structure, AptitudesAssocieesDto Aptitudes, IReadOnlyList<RegleAssocieeDto> Regles, 
    IReadOnlyList<string> Commandements, IReadOnlyList<SecteDto> Sectes, IReadOnlyList<string> Ambiance, 
    IReadOnlyList<TempleDto> Temples, IReadOnlyList<PersonnaliteDto> Personnalites, IReadOnlyList<CulteDto> Ordres)
{
    public DieuDto? Patron { get; set; }
}

public record CulteDto(int Id, string Nom, IReadOnlyList<AptitudeDto> Aptitudes, string Description, bool Mineur, string Ambiance);

public record AptitudesAssocieesDto(IReadOnlyList<AptitudeDto> Inities, IReadOnlyList<AptitudeDto> PretesSansOrdre);

public record RegleAssocieeDto(string Titre, string Description);

public record SecteDto(string Nom, string Description);

public record TempleDto(string Nom, string Description);

public record PersonnaliteDto(string Nom, string Description);
