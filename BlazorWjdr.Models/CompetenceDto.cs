namespace BlazorWjdr.Models
{
    using System.Linq;
    using System.Collections.Generic;

    public class CompetenceDto
    {
        public int Id { get; init; }
        
        public string Nom { get; init; } = null!;
        public string NomPourRecherche { get; set; } = "";
        public List<string> MotsClefDeRecherche { get; set; } = new();

        public string? Specialisation { get; init; }
        public string Resume { private get; init; } = null!;
        public string ResumeComplet { get; private set; } = "";
        public bool EstUneCompetenceDeBase { get; init; }
        public string CaracteristiqueAssociee { get; init; } = null!;
        public List<TalentDto> TalentsLies { get; set; } = new();
        public List<TraitDto> TraitsLies { get; set; } = new();
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

    public class CompetenceAcquise
    {
        private CompetenceAcquise(CompetenceDto competence, int niveau)
        {
            Competence = competence;
            Niveau = niveau;
        }

        public CompetenceDto Competence { get; private init; }
        private int Niveau { get; set; }

        public string Detail => Competence.Nom + (Niveau == 1 ? "" : $" (+{Niveau - 1}0%)");

        public static CompetenceAcquise[] GetList(CompetenceDto[] competences)
        {
            var liste = new List<CompetenceAcquise>();
            foreach (var competence in competences)
            {
                var ca = liste.FirstOrDefault(c => c.Competence == competence);
                if (ca == null)
                    liste.Add(new CompetenceAcquise(competence, 1));
                else
                    ca.Niveau += 1;
            }
            return liste.OrderBy(c => c.Detail).ToArray();
        }
    }
}
