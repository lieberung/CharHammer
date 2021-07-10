namespace BlazorWjdr.Models
{
    public class LieuDto
    {
        public int Id { get; init; }
        
        public LieuTypeDto TypeDeLieu { get; init; }
        
        public int? ParentId { get; init; }
        public LieuDto? Parent { get; set; }
        
        public string Nom { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Image => $"lieux/{Id}.png";
    }
}
