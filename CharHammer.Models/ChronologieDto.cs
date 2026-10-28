namespace CharHammer.Models;

public class ChronologieDto(int Debut, int? Fin, string Resume, string Titre, string Commentaire, IReadOnlyList<ReferenceDto> Sources, IReadOnlyList<DomaineDto> Domaines)
{
    public int Debut { get; } = Debut;
    public int? Fin { get; } = Fin;
    public string Resume { get; } = Resume;
    public string Titre { get; } = Titre;
    public string Commentaire { get; } = Commentaire;
    public IReadOnlyList<ReferenceDto> Sources { get; } = Sources;
    public IReadOnlyList<DomaineDto> Domaines { get; } = Domaines;

    public string Periode => $"{Debut}{(Fin.HasValue ? $" ~ {Fin}" : "")}";
}

public class DomaineDto(int Id, string Nom)
{
    public int Id { get; } = Id;
    public string Nom { get; } = Nom;
}