namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

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
        public CompetenceDto? Parent { get; set; }
        public readonly List<CompetenceDto> SousElements = new();

        public void SetResume()
        {
            ResumeComplet = GetResume();
        }

        private string GetResume()
        {
            if (!string.IsNullOrWhiteSpace(Resume))
                return Resume;
            if (Parent == null || string.IsNullOrWhiteSpace(Parent.Resume))
                return "";
            return Parent.Resume;
        }
    }
}
