namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

    public class TalentDto
    {
        public int Id { get; init; }
        
        public string Nom { get; init; } = null!;
        public string NomPourRecherche { get; set; } = "";
        public List<string> MotsClefDeRecherche { get; set; } = new();
        
        public string Resume { get; init; } = null!;
        public string Description { get; init; } = null!;
        public bool Trait { get; init; }
        public string? Specialisation { get; init; }
        public bool Ignore { get; init; }
        public string Max { get; init; } = null!;
        public string Tests { get; init; } = null!;
        
        public int? TalentParentId { get; init; }
        public TalentDto? Parent { get; set; }
        public readonly List<TalentDto> SousElements = new();

        public List<CompetenceDto> CompetencesLiees { get; set; } = new ();
    }
}
