using System.Collections.Generic;

namespace BlazorWjdr.Models
{
    public class RegleDto
    {
        public int Id { get; init; }
        public bool Html { get; init; }
        public string Titre { get; init; } = null!;
        public int[] ReglesId { get; init; } = null!;
        public RegleDto[] SousRegles { get; set; } = null!;
        public CarriereDto[] Carrieres { get; init; } = null!;
        public CompetenceDto[] Competences { get; init; } = null!;
        public List<CompetenceDto[]> ChoixCompetences { get; init; } = null!;
        public TalentDto[] Talents { get; init; } = null!;
        public List<TalentDto[]> ChoixTalents { get; init; } = null!;
        public BestioleDto[] Bestioles { get; init; } = null!;
        public TableDto[] Tables { get; init; } = null!;
        public LieuDto[] Lieux { get; init; } = null!;
        public string Regle { get; init; } = null!;
    }
}