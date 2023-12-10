using System.Collections.Generic;

namespace CharHammer.Models;

public record SortilegeDto(
    int Id, IEnumerable<AptitudeDto> Aptitudes, string Nom, string Type, string Distance,
    string Cible, string Duree, string? Ingredient, string Effet, int? NS);