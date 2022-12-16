namespace BlazorWjdr.Models;

public class LieuTypeDto
{
    public int Id { get; init; }
    public string Nom { get; init; } = null!;
    
    public int? ParentId { get; init; }
    public LieuTypeDto? Parent { get; set; }
}
