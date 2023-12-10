using System.Collections.Generic;

namespace CharHammer.Models;

public record ChronologieDto(int Debut, int? Fin, string Resume, string Titre, string Commentaire, IEnumerable<ReferenceDto> Sources)
{
    public string Periode => $"{Debut}{(Fin.HasValue ? $" ~ {Fin}" : "")}";
}
