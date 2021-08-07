using System.Linq;

namespace BlazorWjdr.Models
{
    public class BestioleDto
    {
        public int Id { get; init; }
        public bool EstUnPersonnage { get; init; }
        public bool EstUnPersonnageJoueur { get; init; }
        
        public ProfilDto ProfilActuel { get; init; } = null!;
        public int Userid { get; init; }
        public string Nom { get; init; } = null!;
        public string Histoire { get; init; } = null!;
        public string Commentaire { get; init; } = "";
        public CompetenceAcquise[] CompetencesAcquises { get; init; } = null!;

        public TalentDto[] Talents { get; init; } = null!;
        public LieuDto[] Origines { get; init; } = null!;
        public TraitDto[] Traits { get; init; } = null!;
        public RaceDto Race { get; init; } = null!;
        public int[] MembreDe { get; init; } = null!;
        public int? Poids { get; init; }
        public int? Taille { get; init; }
        public int? Age { get; init; }
        public int Sexe { get; init; }
        public string Psychologie { get; init; } = null!;
        
        // Personnage
        public CarriereDto? CarriereDuPere { get; set; }
        public CarriereDto? CarriereDeLaMere { get; set; }
        public int? fk_signeastralid { get; set; }
        public string? FreresEtSoeurs { get; set; }
        public int MainDirectrice { get; set; }
        public bool Mort { get; set; }
        public CarriereDto[] CheminementPro { get; set; } = null!;
        public string? Cheveux { get; set; }
        public string? Yeux { get; set; }

        public CarriereDto? CarriereActuelle => CheminementPro.LastOrDefault();
        
        // PJ
        public System.DateTime? DateDeCreation { get; set; }
        public string Joueur { get; set; } = null!;
        public int XpActuel { get; set; }
        public int XpTotal { get; set; }
        public ProfilDto? ProfilInitial { get; set; }
        
        public string Equipement
        {
            get
            {
                //if (!CheminementPro.Any()) return "";
                return string.Join(", ",  
                    CheminementPro.SelectMany(c => c.Dotations.Split(", ")).Distinct().OrderBy(s => s).ToArray()
                );
            }
        }
    }
}
