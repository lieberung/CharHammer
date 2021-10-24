// ReSharper disable UnusedAutoPropertyAccessor.Global
using System.Linq;

namespace BlazorWjdr.Models
{
    public class BestioleDto
    {
        public int Id { get; init; }
        public bool EstUnPersonnage => CheminementPro.Any();
        public bool EstUnPersonnageJoueur => Userid != 0;
        public bool EstUnPersonnageNonJoueur => EstUnPersonnage && !EstUnPersonnageJoueur;
        public bool EstUneCreature => !EstUnPersonnage;
        public AptitudeDto? Gabarit { get; set; }

        public ProfilDto ProfilActuel { get; init; } = null!;
        
        public int Userid { get; init; }
        public string Nom { get; init; } = null!;
        public string Histoire { get; init; } = null!;
        public string Ambitions { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Notes { get; init; } = "";
        
        public AptitudeAcquise[] AptitudesAcquises { get; init; } = null!;
        public AptitudeAcquise[] AptitudesOptionnels { get; init; } = null!;
        
        public AptitudeAcquise[] Competences => AptitudesAcquises.Where(a => a.Aptitude.EstUneCompetence).ToArray();
        public AptitudeAcquise[] Talents => AptitudesAcquises.Where(a => a.Aptitude.EstUnTalent).ToArray();
        public AptitudeAcquise[] Traits => AptitudesAcquises.Where(a => a.Aptitude.EstUnTrait).ToArray();

        public LieuDto[] Origines { get; init; } = null!;
        public RaceDto Race { get; init; } = null!;
        public string[] MembreDe { get; init; } = null!;
        public int? Poids { get; init; }
        public int? Taille { get; init; }
        public int? Age { get; init; }
        public int Sexe { get; init; }
        public string Psychologie { get; init; } = null!;

        public ArmeDto[] Armes { get; init; } = null!;
        public ArmureDto[] Armures { get; init; } = null!;
        public EquipementDto[] Equipement { get; init; } = null!;

        // Personnage
        public CarriereDto? CarriereDuPere { get; init; }
        public CarriereDto? CarriereDeLaMere { get; init; }
        public int? SigneAstralId { get; set; }
        public string? FreresEtSoeurs { get; init; }
        public int MainDirectrice { get; init; }
        public bool Mort { get; set; }
        public CarriereDto[] CheminementPro { get; init; } = null!;
        public string? Cheveux { get; init; }
        public string? Yeux { get; init; }

        public CarriereDto? CarriereActuelle => CheminementPro.LastOrDefault();
        
        // PJ
        public string DateDeCreation { get; init; } = null!;
        public string Joueur { get; init; } = null!;
        public int XpActuel { get; init; }
        public int XpTotal { get; init; }
        public ProfilDto? ProfilInitial { get; init; }

        public int Blessures { get; set; }
        public string BlessuresDetailDuCalcul { get; set; } = "";
        public string BlessuresFormuleDeCalcul { get; set; } = "";

        public string EquipementDeCarrieres
        {
            get
            {
                return string.Join(", ",  
                    CheminementPro.SelectMany(c => c.Dotations.Split(", ")).Distinct().OrderBy(s => s).ToArray()
                );
            }
        }
    }
}
