namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

    public class TalentDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
        public string ResumeComplet { get; private set; } = "";
        public string Resume { get; init; } = null!;
        public string Description { get; init; } = null!;
        public bool Trait { get; init; }
        public string? Specialisation { get; init; }
        public bool Ignore { get; init; }

        public int? TalentParentId { get; init; }
        public TalentDto? Parent { get; set; }
        public readonly List<TalentDto> SousElements = new();

        public List<CompetenceDto> CompetencesLiees { get; set; } = new ();
        
        #region Resume, Description, ResumeComplet
        
        private string GetResume()
        {
            if (!string.IsNullOrWhiteSpace(Resume))
                return Resume;
            if (Parent == null || string.IsNullOrWhiteSpace(Parent.Resume))
                return "";
            return Parent.Resume;
        }
        
        public string GetDescritpion()
        {
            if (!string.IsNullOrWhiteSpace(Description))
                return Description;
            if (Parent == null || string.IsNullOrWhiteSpace(Parent.Description))
                return "";
            return Parent.Description;
        }
        
        #endregion
    }
}
