namespace CharHammer.Models;

public record TableDto(int Id, string Titre, string Description, string[] StylesHeader, string[] StylesRows, string[][] Lignes);