using System;

namespace BlazorWjdr.Models;

using System.Collections.Generic;
using System.Linq;

public class TirageDto
{
    public RaceDto Race { get; init; } = null!;
    public int Facteur { get; init; }
}

public class SourceDto
{
    public ReferenceDto Book { get; init; } = null!;
    public string Info { get; init; } = null!;
}

public class CarriereDto
{
    public int Id { get; init; }
    public int? NiveauSpecifie { get; init; }
    public string Groupe { get; set; } = null!;
    public string Statut { get; set; } = null!;

    public string StatutPretty()
    {
        return string.Concat(Statut[..1] switch
        {
            "B" => "Bronze",
            "A" => "Argent",
            "O" => "Or",
            _ => "inconnu"
        }, " ", Statut.AsSpan(1,1));
    }

    public string SalaireHebdo
    {
        get
        {
            if (Statut == "") return "";
            var echelon = Statut[..1];
            var standing = int.Parse(Statut.Substring(1, 1));
            var calcul = echelon switch
            {
                "B" => "2d10 sous de cuivre",
                "A" => "1d10 pistoles d'argent",
                "O" => "1 couronne d'or",
                _ => "inconnu"
            };
            return $"Revenus pour une semaine (8 jours) de travail :\n{standing} x [{calcul}]";
        }
    }

    public string Nom { get; init; } = null!;
    public string NomAnglais { get; init; } = null!;
    public List<string> MotsClefDeRecherche { get; init; } = null!;
    public string Description { get; set; } = null!;
    public CitationDto[] Ambiance { get; set; } = Array.Empty<CitationDto>();
    public string Image { get; init; } = null!;
    public bool EstUneCarriereDeBase => TirageInitial.Any() ||  Parent is { EstUneCarriereDeBase: true };
    public bool EstUneCarriereAvancee => !EstUneCarriereDeBase;
    public string Restriction { get; set; } = null!;
    public string Leitmotiv { get; set; } = null!;
    public int[] DebouchesIds { get; init; } = null!;
    public int[] AvancementsIds { get; init; } = null!;
    public string Dotations { get; init; } = null!;
    public int? CarriereMereId { get; init; }

    public IEnumerable<string> Images { get; } = Array.Empty<string>();

    public ProfilDto PlanDeCarriere { get; init; } = null!;

    public AptitudeDto? CompetenceDeMetier { get; set; }
    public List<AptitudeDto> Aptitudes { get; init; } = new();
    public List<AptitudeDto[]> AptitudesChoix { get; init; } = new();

    public IEnumerable<AptitudeDto> Competences => Aptitudes.Where(a => a.EstUneCompetence).OrderBy(a => a.NomComplet).ToList();
    public IEnumerable<AptitudeDto> Talents => Aptitudes.Where(a => a.EstUnTalent).OrderBy(a => a.NomComplet).ToList();
    public IEnumerable<AptitudeDto> Traits => Aptitudes.Where(a => a.EstUnTrait).OrderBy(a => a.NomComplet).ToList();

    public List<AptitudeDto[]> ChoixCompetences => AptitudesChoix.Where(choix => choix.First().EstUneCompetence).ToList();
    public List<AptitudeDto[]> ChoixTalents => AptitudesChoix.Where(choix => choix.First().EstUnTalent).ToList();
    public IEnumerable<AptitudeDto[]> ChoixTraits => AptitudesChoix.Where(choix => choix.First().EstUnTrait);
    
    public bool ProposeAuMoinsUnTrait => Traits.Any() || ChoixTraits.Any();
    
    public CarriereDto? Parent { get; set; }
    public readonly List<CarriereDto> SousElements = new();

    public List<CarriereDto> Debouches = new();
    public List<CarriereDto> Filieres = new();

    public List<CarriereDto> FilieresDAvancement = new();
    public List<CarriereDto> DebouchesDAvancements = new();

    public TirageDto[] TirageInitial { get; init; } = null!;
    public SourceDto? Source { get; init; }

    public int Niveau
    {
        get
        {
            if (NiveauSpecifie.HasValue)
                return NiveauSpecifie.Value;
            if (EstUneCarriereDeBase)
                return 1;
            return Filieres.Any(f => f.EstUneCarriereDeBase) ? 2 : 3;
        }
    }

    // public int ScoreAcademique { get; }
    // public int ScoreMartialAuContact { get; }
    // public int ScoreMartialADistance { get; set; }
    // public int ScoreCavalerie { get; set; }
    // public int ScoreDeLOmbre { get; set; }
    // public int ScoreSocial { get; set; }
    // public int ScoreCommerce { get; set; }
    // public int ScoreArcanique { get; set; }
    // public int ScoreArtisanat { get; set; }
    // public int ScoreRodeur { get; set; }
    // public int ScoreMaritime { get; set; }
    // public int ScorePoudreNoire { get; set; }
    // public int ScoreAmiDesBetes { get; set; }

    public IEnumerable<AptitudeDto> AptitudesPourScore {
        get
        {
            var list = new List<AptitudeDto>();
            list.AddRange(Aptitudes);
            list.AddRange(AptitudesChoix.SelectMany(c => c));
            return list;
        }
    }
}
