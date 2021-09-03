using System.Collections.Generic;

namespace BlazorWjdr.Models
{
    public class RaceDto
    {
        public int Id { get; init; }
        public LieuDto[] Lieux { get; init; } = null!;
        public bool PourPj { get; init; }
        public bool GroupOnly { get; init; }
        public string NomMasculin { get; init; } = null!;
        public string NomFeminin { get; init; } = null!;
        public string Description { get; init; } = null!;

        public int? ParentId { get; init; }
        public RaceDto? Parent { get; set; }
        public readonly List<RaceDto> SousElements = new();
        
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
