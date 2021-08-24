using System.Collections.Generic;

namespace BlazorWjdr.Models
{
    public class LieuDto
    {
        public int Id { get; init; }

        public LieuTypeDto TypeDeLieu { get; init; } = null!;
        
        public int? ParentId { get; init; }
        public LieuDto? Parent { get; set; }
        public readonly List<LieuDto> SousElements = new();
        
        public string Nom { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Image => $"lieux/{Id}.png";

        public int ParentsCount {
            get
            {
                if (Parent == null)
                    return 0;
                return Parent.ParentsCount + 1;
            }
        }
    }
}
