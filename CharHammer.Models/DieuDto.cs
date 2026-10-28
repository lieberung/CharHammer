namespace CharHammer.Models;

public class DieuDto(int Id, string Nom, string Domaines, string Fideles, string Symboles, string Histoire, 
    string Commentaire, int? PatronId, LieuDto? Siege, string Chef, string Fetes, string LivresSaints, 
    string Intro, string Penitences, string Culte, string Dogme, string Initiation, string Pretrise, 
    string Cultistes, string Structure, AptitudesAssocieesDto Aptitudes, IReadOnlyList<RegleAssocieeDto> Regles, 
    IReadOnlyList<string> Commandements, IReadOnlyList<SecteDto> Sectes, IReadOnlyList<string> Ambiance, 
    IReadOnlyList<TempleDto> Temples, IReadOnlyList<PersonnaliteDto> Personnalites, IReadOnlyList<CulteDto> Ordres)
{
    public int Id { get; } = Id;
    public string Nom { get; } = Nom;
    public string Domaines { get; } = Domaines;
    public string Fideles { get; } = Fideles;
    public string Symboles { get; } = Symboles;
    public string Histoire { get; } = Histoire;
    public string Commentaire { get; } = Commentaire;
    public int? PatronId { get; } = PatronId;
    public LieuDto? Siege { get; } = Siege;
    public string Chef { get; } = Chef;
    public string Fetes { get; } = Fetes;
    public string LivresSaints { get; } = LivresSaints;
    public string Intro { get; } = Intro;
    public string Penitences { get; } = Penitences;
    public string Culte { get; } = Culte;
    public string Dogme { get; } = Dogme;
    public string Initiation { get; } = Initiation;
    public string Pretrise { get; } = Pretrise;
    public string Cultistes { get; } = Cultistes;
    public string Structure { get; } = Structure;
    public AptitudesAssocieesDto Aptitudes { get; } = Aptitudes;
    public IReadOnlyList<RegleAssocieeDto> Regles { get; } = Regles;
    public IReadOnlyList<string> Commandements { get; } = Commandements;
    public IReadOnlyList<SecteDto> Sectes { get; } = Sectes;
    public IReadOnlyList<string> Ambiance { get; } = Ambiance;
    public IReadOnlyList<TempleDto> Temples { get; } = Temples;
    public IReadOnlyList<PersonnaliteDto> Personnalites { get; } = Personnalites;
    public IReadOnlyList<CulteDto> Ordres { get; } = Ordres;

    public DieuDto? Patron { get; set; }
}

public class CulteDto(int Id, string Nom, IReadOnlyList<AptitudeDto> Aptitudes, string Description, bool Mineur, string Ambiance)
{
    public int Id { get; } = Id;
    public string Nom { get; } = Nom;
    public IReadOnlyList<AptitudeDto> Aptitudes { get; } = Aptitudes;
    public string Description { get; } = Description;
    public bool Mineur { get; } = Mineur;
    public string Ambiance { get; } = Ambiance;
}

public record AptitudesAssocieesDto(IReadOnlyList<AptitudeDto> Inities, IReadOnlyList<AptitudeDto> PretesSansOrdre);

public record RegleAssocieeDto(string Titre, string Description);

public record SecteDto(string Nom, string Description);

public record TempleDto(string Nom, string Description);

public record PersonnaliteDto(string Nom, string Description);
