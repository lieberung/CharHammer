// ReSharper disable UnusedAutoPropertyAccessor.Global
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
        public string Description { get; init; } = null!;
        public string Notes { get; init; } = "";
        
        public AptitudeAcquise[] AptitudesAcquises { get; init; } = null!;
        
        public AptitudeAcquise[] Competences => AptitudesAcquises.Where(a => a.Aptitude.EstUneCompetence).ToArray();
        public AptitudeAcquise[] Talents => AptitudesAcquises.Where(a => a.Aptitude.EstUnTalent).ToArray();
        public AptitudeAcquise[] Traits => AptitudesAcquises.Where(a => a.Aptitude.EstUnTrait).ToArray();

        public LieuDto[] Origines { get; init; } = null!;
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
        public int? SigneAstralId { get; set; }
        public string? FreresEtSoeurs { get; set; }
        public int MainDirectrice { get; set; }
        public bool Mort { get; set; }
        public CarriereDto[] CheminementPro { get; set; } = null!;
        public string? Cheveux { get; set; }
        public string? Yeux { get; set; }

        public CarriereDto? CarriereActuelle => CheminementPro.LastOrDefault();
        
        // PJ
        public string DateDeCreation { get; init; } = null!;
        public string Joueur { get; set; } = null!;
        public int XpActuel { get; set; }
        public int XpTotal { get; set; }
        public ProfilDto? ProfilInitial { get; init; }

        // ToDo : gérer les gabarits et Dur à cuir
        public int Blessures
        {
            get
            {
                if (Race.Id == 26) // Halfling
                    return (2 * ProfilActuel.Be) + ProfilActuel.Bfm;
                return ProfilActuel.Bf + (2 * ProfilActuel.Be) + ProfilActuel.Bfm;
            }
        }

        public string BlessuresCalculDetail
        {
            get
            {
                if (Race.Id == 26) // Halfling
                    return $"(2x{ProfilActuel.Be})+{ProfilActuel.Bfm}";
                return $"{ProfilActuel.Bf}+(2x{ProfilActuel.Be})+{ProfilActuel.Bfm}";
            }
        }
        public string BlessuresCalculFormule
        {
            get
            {
                if (Race.Id == 26) // Halfling
                    return $"(2xBE)+BFM";
                return $"BF+(2xBE)+BFM";
            }
        }

        public string Equipement
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
