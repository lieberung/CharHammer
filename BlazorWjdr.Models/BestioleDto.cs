// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Generic;
using System.Linq;
using static System.Int32;

namespace BlazorWjdr.Models;

public class BestioleDto
{
    public int Id { get; init; }
    public bool EstUnPersonnage => CheminementPro.Any();
    public bool EstUnPersonnageJoueur => Userid != 0 && Userid != 999;
    public bool EstUnPersonnagePretire => EstUnPersonnage && Userid == 999;
    public bool EstUnPersonnageNonJoueur => EstUnPersonnage && Userid == 0;
    public bool EstUnArchetype => EstUnPersonnageNonJoueur && Nom == "Archétype";
    public bool EstUneCreature => !EstUnPersonnage;
    public AptitudeDto? Gabarit { get; set; }

    public ProfilDto ProfilActuel { get; init; } = null!;
    
    public int Userid { get; init; }
    public string Nom { get; init; } = null!;
    public string NomCourt { get; init; } = null!;
    public string Histoire { get; init; } = null!;
    public string[] Ambitions { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Notes { get; init; } = "";
    
    public AptitudeAcquiseDto[] AptitudesAcquises { get; init; } = null!;
    public AptitudeAcquiseDto[] AptitudesOptionnels { get; init; } = null!;
    
    public AptitudeAcquiseDto[] Competences => AptitudesAcquises.Where(a => a.Aptitude.EstUneCompetence).ToArray();
    public AptitudeAcquiseDto[] Talents => AptitudesAcquises.Where(a => a.Aptitude.EstUnTalent).ToArray();
    public AptitudeAcquiseDto[] Traits => AptitudesAcquises.Where(a => a.Aptitude.EstUnTrait).ToArray();

    public LieuDto[] Origines { get; init; } = null!;
    public RaceDto Race { get; init; } = null!;
    public string[] MembreDe { get; init; } = null!;
    public int? Poids { get; init; }
    public int? Taille { get; init; }
    public int? Age { get; init; }
    public int? Sexe { get; init; }
    public bool Masquer { get; init; }
    public string Psychologie { get; init; } = null!;

    public ArmeDto[] Armes { get; init; } = null!;
    public ArmureDto[] Armures { get; init; } = null!;
    public EquipementDto[] Equipement { get; init; } = null!;
    public SortilegeDto[] Sorts { get; init; } = null!;

    public ProtectionsDto Protections {
        get
        {
            var synthese = new ProtectionsDto();
            foreach (var armure in Armures)
            {
                var zones = armure.Zones.ToLower();
                TryParse(armure.Pa, out var pa);
                if (pa == 0) continue;
                if (zones.Contains("toutes") || zones.Contains("tête"))
                    synthese.Tete += pa;
                if (zones.Contains("toutes") || zones.Contains("bras"))
                    synthese.Bras += pa;
                if (zones.Contains("toutes") || zones.Contains("torse"))
                    synthese.Torse += pa;
                if (zones.Contains("toutes") || zones.Contains("jambes"))
                    synthese.Jambes += pa;
            }
            var armureNaturelle = AptitudesAcquises.SingleOrDefault(aa => aa.Aptitude.Id == 4001);
            if (armureNaturelle != null)
            {
                synthese.Tete += armureNaturelle.Niveau;
                synthese.Bras += armureNaturelle.Niveau;
                synthese.Torse += armureNaturelle.Niveau;
                synthese.Jambes += armureNaturelle.Niveau;
            }
            return synthese;
        }
    }

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

    public class ProtectionsDto
    {
        public int Tete { get; set; }
        public int Bras { get; set; }
        public int Torse { get; set; }
        public int Jambes { get; set; }

        private int Total => Tete + Bras + Torse + Jambes;
        public bool Aucune => Total == 0;
        public override string ToString()
        {
            if (Aucune) return "";
            
            var toStr = new List<string>();
            if (Tete != 0)
                toStr.Add($"Tête {Tete}");
            if (Bras != 0)
                toStr.Add($"Bras {Bras}");
            if (Torse != 0)
                toStr.Add($"Torse {Torse}");
            if (Jambes != 0)
                toStr.Add($"Jambes {Jambes}");

            if (Tete == Bras && Tete == Torse && Tete == Jambes)
                return $"Toutes les zones {Tete}";

            return string.Join(", ", toStr);
        }
    }
}
