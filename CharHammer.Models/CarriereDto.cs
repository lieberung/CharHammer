using System;

namespace CharHammer.Models;

public record TirageDto(RaceDto Race, int Facteur);

public record SourceDto(ReferenceDto Book, string Info);

public record CarriereDto(int Id, int? NiveauSpecifie, string Nom, string NomAnglais, string Image,
    IEnumerable<string> MotsClefDeRecherche, IEnumerable<int> DebouchesIds, IEnumerable<int> AvancementsIds,
    string Dotations, int? CarriereMereId, ProfilDto PlanDeCarriere,
    IEnumerable<AptitudeDto> Aptitudes, IEnumerable<IEnumerable<AptitudeDto>> AptitudesChoix,
    IEnumerable<TirageDto> TirageInitial, SourceDto? Source
) : ISearchable
{
    public string Groupe = null!;
    public string Statut = null!;
    public string Description = null!;
    public IEnumerable<CitationDto> Ambiance = [];
    public string Restriction = null!;
    public string Leitmotiv = null!;
    public AptitudeDto? CompetenceDeMetier;

    public CarriereDto? Parent;
    public IEnumerable<CarriereDto> SousElements = [];

    public IEnumerable<CarriereDto> Debouches = [];
    public IEnumerable<CarriereDto> Filieres = [];

    public IEnumerable<CarriereDto> FilieresDAvancement = [];
    public IEnumerable<CarriereDto> DebouchesDAvancements = [];

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

    public bool EstUneCarriereDeBase => TirageInitial.Any() ||  Parent is { EstUneCarriereDeBase: true };
    public bool EstUneCarriereAvancee => !EstUneCarriereDeBase;

    public IEnumerable<AptitudeDto> Competences => Aptitudes.Where(a => a.EstUneCompetence).OrderBy(a => a.NomComplet);
    public IEnumerable<AptitudeDto> Talents => Aptitudes.Where(a => a.EstUnTalent).OrderBy(a => a.NomComplet);
    public IEnumerable<AptitudeDto> Traits => Aptitudes.Where(a => a.EstUnTrait).OrderBy(a => a.NomComplet);

    public IEnumerable<IEnumerable<AptitudeDto>> ChoixCompetences => AptitudesChoix.Where(choix => choix.First().EstUneCompetence);
    public IEnumerable<IEnumerable<AptitudeDto>> ChoixTalents => AptitudesChoix.Where(choix => choix.First().EstUnTalent);
    public IEnumerable<IEnumerable<AptitudeDto>> ChoixTraits => AptitudesChoix.Where(choix => choix.First().EstUnTrait);
    
    public bool ProposeAuMoinsUnTrait => Traits.Any() || ChoixTraits.Any();

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

    public IEnumerable<AptitudeDto> AptitudesPourScore => Aptitudes.Union(AptitudesChoix.SelectMany(c => c));

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
}
