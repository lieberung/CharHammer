using System.Collections.Generic;

namespace BlazorWjdr.Models
{
    public class LieuDto
    {
        public int Id { get; init; }

        public LieuTypeDto TypeDeLieu { get; init; } = null!;
        
        public int? ParentId { get; init; }
        public LieuDto? Parent { get; set; }
        public List<LieuDto> SousElements = new();
        
        public string Nom { get; init; } = null!;
        public string Population { get; init; } = null!;
        public string Allegeance { get; init; } = null!;
        public string Industrie { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Image => $"lieux/{Id}.png";
        public bool Ignorer { get; init; }

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
