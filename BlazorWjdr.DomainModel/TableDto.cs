namespace BlazorWjdr.Models
{
    public class TableDto
    {
        public int Id { get; init; }
        public string Titre { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string[] StylesHeader { get; init; } = null!;
        public string[] StylesRows { get; init; } = null!;
        public string[][] Lignes { get; init; } = null!;
    }
}