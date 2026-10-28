namespace CharHammer.Models;

public class TableDto(int Id, string Titre, string Description, string[] StylesHeader, string[] StylesRows, string[][] Lignes)
{
    public int Id { get; } = Id;
    public string Titre { get; } = Titre;
    public string Description { get; } = Description;
    public string[] StylesHeader { get; } = StylesHeader;
    public string[] StylesRows { get; } = StylesRows;
    public string[][] Lignes { get; } = Lignes;
}