namespace BlazorWjdr.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class CarriereDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
        public List<string> MotsClefDeRecherche { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Image { get; init; } = null!;
        public bool EstUneCarriereAvancee { get; init; }
        public string Restriction { get; init; } = null!;
        public string Source { get; init; } = null!;
        public int[] DebouchesIds { get; init; } = null!;
        public string Dotations { get; init; } = null!;
        public int? CarriereMereId { get; init; }

        public ProfilDto PlanDeCarriere { get; init; } = null!;
        
        public List<TalentDto> Talents { get; init; } = null!;
        public List<CompetenceDto> Competences { get; init; } = null!;

        public List<TalentDto[]> ChoixTalents { get; init; } = null!;
        public List<CompetenceDto[]> ChoixCompetences { get; init; } = null!;

        public CarriereDto? Parent { get; set; }
        public List<CarriereDto> Debouches = new();
        public List<CarriereDto> Filieres = new();
        public readonly List<CarriereDto> SousElements = new();

        public ReferenceDto? SourceLivre { get; init; }

        public int ScoreAcademique { get; set; }
        public int ScoreMartialAuContact { get; set; }
        public int ScoreMartialADistance { get; set; }
        public int ScoreCavalerie { get; set; }
        public int ScoreDeLOmbre { get; set; }
        public int ScoreSocial { get; set; }
        public int ScoreCommerce { get; set; }
        public int ScoreArcanique { get; set; }
        public int ScoreArtisanat { get; set; }
        public int ScoreRodeur { get; set; }
        public int ScoreMaritime { get; set; }
        public int ScorePoudreNoire { get; set; }
        public int ScoreAmiDesBetes { get; set; }

        public List<CompetenceDto> CompetencesPourScore {
            get
            {
                var list = new List<CompetenceDto>();
                list.AddRange(Competences);
                list.AddRange(ChoixCompetences.SelectMany(c => c));
                return list;
            }
        }
        
        public List<TalentDto> TalentsPourScore {
            get
            {
                var list = new List<TalentDto>();
                list.AddRange(Talents);
                list.AddRange(ChoixTalents.SelectMany(c => c));
                return list;
            }
        }
    }
}
