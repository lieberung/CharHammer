namespace BlazorWjdr.Models
{
    using System.Linq;
    using System.Collections.Generic;

    public class AptitudeDto
    {
        public int Id { get; init; }
        public bool Ignore { get; init; }
        public bool Martial { get; init; }
        
        public string Nom { get; init; } = null!;
        public string? NomEn { get; init; }
        public string? Spe { get; init; }
        public string NomPourRecherche { get; set; } = "";
        public List<string> MotsClefDeRecherche { get; set; } = new();
        
        public string Categ { get; init; } = null!;
        public string CategSpe { get; init; } = null!;
        
        public string Resume { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string ResumeComplet { get; private set; } = "";
        public string DescriptionComplete { get; private set; } = "";

        public bool EstUneCompetence => Categ == "skill";
        public bool EstUneCompetenceDeBase => EstUneCompetence && CategSpe == "bas";
        public bool EstUnTalent => Categ == "talent";
        public bool EstUnTrait => Categ == "trait";

        // Competence
        public string CaracteristiqueAssociee { get; set; } = null!;
        // Talent
        public string? Max { get; init; }
        public string Tests { get; set; } = null!;
        // Trait
        public int Severite { get; init; }
        public string Guerison { get; init; } = null!;
        public bool? Contagieux { get; init; }

        public int? AptitudeMereId { get; init; }
        public AptitudeDto? Parent { get; set; }
        public readonly List<AptitudeDto> SousElements = new();
        public List<int> AptitudesLieesIds { get; init; } = null!;
        public List<AptitudeDto> AptitudesLiees { get; set; } = new ();

        public List<int> IncompatiblesIds { get; init; } = null!;
        public List<AptitudeDto> Incompatibles { get; set; } = null!;
        public List<AptitudeDto> CompetencesLiees => AptitudesLiees.Where(a => a.EstUneCompetence).ToList();
        public List<AptitudeDto> TalentsLies => AptitudesLiees.Where(a => a.EstUnTalent).ToList();
        public List<AptitudeDto> TraitsLies => AptitudesLiees.Where(a => a.EstUnTrait).ToList();

        public string Icon => EstUneCompetence ? "target" : EstUnTalent ? "brush" : EstUnTrait ? "droplet" : "error";
        public string NomComplet => Nom + (string.IsNullOrWhiteSpace(Spe) ? "" : $" : {Spe}");
        public string CategSpeSexy {
            get
            {
                if (string.IsNullOrWhiteSpace(CategSpe))
                    return "non classé";
                return CategSpe switch
                {
                    "trait" => "signe distinctif",
                    "nevrose" => "névrose",
                    "capacite" => "capacité",
                    "nature_avantageuse" => "prédisposition",
                    "resistance" => "résistance",
                    "animosite" => "animosité",
                    _ => CategSpe
                };
            }
        }
        
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
        
        public void SetDescription()
        {
            DescriptionComplete = GetDescription();
        }

        private string GetDescription()
        {
            if (!string.IsNullOrWhiteSpace(Description))
                return Description;
            if (Parent == null || string.IsNullOrWhiteSpace(Parent.Description))
                return "";
            return Parent.Description;
        }
    }

    public class AptitudeAcquise
    {
        private AptitudeAcquise(AptitudeDto aptitude, int niveau)
        {
            Aptitude = aptitude;
            Niveau = niveau;
        }

        public AptitudeDto Aptitude { get; }
        public int Niveau { get; set; }
        private int ChancesDeSucces { get; set; }
        
        public string Detail
        {
            get
            {
                if (Aptitude.EstUneCompetence)
                {
                    return $"{Aptitude.Nom} ({ChancesDeSucces}%)"; // (+{Niveau * 5}%)
                }
                if (Aptitude.EstUnTalent)
                {
                    var rating = Niveau == 1 ? "" : $" ({Niveau})";
                    return $"{Aptitude.Nom}{rating}";
                }
                var niveau = Niveau == 1 ? "" : $" (+{Niveau})";
                return $"{Aptitude.Nom}{niveau}";
            }
        }

        public static AptitudeAcquise[] GetList(IEnumerable<AptitudeDto> aptitudes, ProfilDto profil)
        {
            var liste = new List<AptitudeAcquise>();
            foreach (var apt in aptitudes)
            {
                var ca = liste.FirstOrDefault(c => c.Aptitude == apt);
                if (ca == null)
                    liste.Add(new AptitudeAcquise(apt, 1));
                else
                    ca.Niveau += 1;
            }

            foreach (var aptAcq in liste.Where(aa => aa.Aptitude.EstUneCompetence))
            {
                aptAcq.ChancesDeSucces = profil.GetStat(aptAcq.Aptitude.CaracteristiqueAssociee) + aptAcq.Niveau * 5;
            }
            return liste.OrderBy(c => c.Detail).ToArray();
        }
    }
}
