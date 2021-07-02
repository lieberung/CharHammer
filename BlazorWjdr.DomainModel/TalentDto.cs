namespace BlazorWjdr.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class TalentDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Resume { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Trait { get; set; }
        public string? Specialisation { get; set; }
        public int? TalentParentId { get; set; }
        public TalentDto? Parent { get; set; }
        public bool Ignore { get; set; }

        public List<CompetenceDto> CompetencesLiees { get; set; } = new List<CompetenceDto>();

        public string CompetencesLieesToString => CompetencesLiees.Any() ?
            string.Join(", ", CompetencesLiees
                .Where(c => c.Ignore == false)
                .OrderBy(t => t.Nom).ThenBy(t => t.Specialisation)
                .Select(t => t.ToString())
                .ToArray())
            : "";
        public override string ToString() => $"{Nom}{(!string.IsNullOrWhiteSpace(Specialisation) ? $" ({Specialisation})" : "")}";
    }
}
