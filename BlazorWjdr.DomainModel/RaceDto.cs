namespace BlazorWjdr.Models
{
    public class RaceDto
    {
        public int Id { get; init; }
        public ProfilDto? Profil { get; init; }
        public LieuDto[] Lieux { get; init; } = null!;
        public bool PourPj { get; init; }
        public bool PourPnj { get; init; }
        public bool GroupOnly { get; init; }
        public string NomMasculin { get; init; } = null!;
        public string NomFeminin { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Traits { get; init; } = null!;

        public int? ParentId { get; init; }
        public RaceDto? Parent { get; set; }

        public string ImageMale => $"{Id}m.png";
        public string ImageFemelle => $"{Id}f.png";
    }
}
