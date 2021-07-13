namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

    public class TalentDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
        public string ResumeComplet { get; private set; } = "";
        public string Resume { private get; init; } = null!;
        public string Description { private get; init; } = null!;
        public bool Trait { get; init; }
        public string? Specialisation { get; init; }
        public bool Ignore { get; init; }

        public int? TalentParentId { get; init; }
        public TalentDto? Parent { get; set; }
        public readonly List<TalentDto> SousElements = new();

        public List<CompetenceDto> CompetencesLiees { get; set; } = new List<CompetenceDto>();
        
        #region Resume, Description, ResumeComplet
        
        private string GetResume()
        {
            if (!string.IsNullOrWhiteSpace(Resume))
                return Resume;
            if (Parent == null || string.IsNullOrWhiteSpace(Parent.Resume))
                return "";
            return Parent.Resume;
        }
        
        private string GetDescritpion()
        {
            if (!string.IsNullOrWhiteSpace(Description))
                return Resume;
            if (Parent == null || string.IsNullOrWhiteSpace(Parent.Description))
                return "";
            return Parent.Description;
        }

        public void SetResumeComplet()
        {
            ResumeComplet = GetResumeComplet();
        }

        private string GetResumeComplet()
        {
            string resumeComplet = GetResume();
            if (!string.IsNullOrWhiteSpace(resumeComplet))
                resumeComplet += "\r\n";
            resumeComplet += GetDescritpion();
            return resumeComplet;
        }
        
        #endregion
    }
}
