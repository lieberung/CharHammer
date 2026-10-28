namespace CharHammer.Models;

public record ChronologieDto(int Debut, int? Fin, string Resume, string Titre, string Commentaire, IReadOnlyList<ReferenceDto> Sources, IReadOnlyList<DomaineDto> Domaines)
{
    public string Periode => $"{Debut}{(Fin.HasValue ? $" ~ {Fin}" : "")}";
}

public record DomaineDto(int Id, string Nom);