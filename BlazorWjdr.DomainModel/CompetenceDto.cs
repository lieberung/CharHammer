namespace BlazorWjdr.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class CompetenceDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
        public string? Specialisation { get; init; }
        public string Resume { private get; init; } = null!;
        public string ResumeComplet { get; private set; } = "";
        public bool EstUneCompetenceDeBase { get; init; }
        public string CaracteristiqueAssociee { get; init; } = null!;
        public List<TalentDto> TalentsLies { get; init; } = new();
        public bool Ignore { get; init; }

        public int? CompetenceMereId { get; init; }
        public CompetenceDto? CompetenceParente { get; set; }
        public readonly List<CompetenceDto> SousElements = new();

        public void SetResume()
        {
            ResumeComplet = GetResume();
        }

        private string GetResume()
        {
            if (!string.IsNullOrWhiteSpace(Resume))
                return Resume;
            if (CompetenceParente == null || string.IsNullOrWhiteSpace(CompetenceParente.Resume))
                return "";
            return CompetenceParente.Resume;
        }
    }
}
