namespace CharHammer.Models;

public record TirageDto(RaceDto Race, int Facteur);

public record SourceDto(ReferenceDto Book, string Info);

public class CarriereDto(int Id, int? NiveauSpecifie, string Nom, string NomAnglais, string Image,
    IReadOnlyList<string> MotsClefDeRecherche, IReadOnlyList<int> DebouchesIds, IReadOnlyList<int> AvancementsIds,
    string Dotations, int? CarriereMereId, ProfilDto PlanDeCarriere,
    IReadOnlyList<AptitudeDto> Aptitudes, IReadOnlyList<IReadOnlyList<AptitudeDto>> AptitudesChoix,
    IReadOnlyList<TirageDto> TirageInitial, SourceDto? Source
) : ISearchable
{
    public int Id { get; } = Id;
    public int? NiveauSpecifie { get; } = NiveauSpecifie;
    public string Nom { get; } = Nom;
    public string NomAnglais { get; } = NomAnglais;
    public string Image { get; } = Image;
    public IReadOnlyList<string> MotsClefDeRecherche { get; } = MotsClefDeRecherche;
    public IReadOnlyList<int> DebouchesIds { get; } = DebouchesIds;
    public IReadOnlyList<int> AvancementsIds { get; } = AvancementsIds;
    public string Dotations { get; } = Dotations;
    public int? CarriereMereId { get; } = CarriereMereId;
    public ProfilDto PlanDeCarriere { get; } = PlanDeCarriere;
    public IReadOnlyList<AptitudeDto> Aptitudes { get; } = Aptitudes;
    public IReadOnlyList<IReadOnlyList<AptitudeDto>> AptitudesChoix { get; } = AptitudesChoix;
    public IReadOnlyList<TirageDto> TirageInitial { get; } = TirageInitial;
    public SourceDto? Source { get; } = Source;

    // Valeurs saisies dans carriere.json. Vides quand elles doivent venir de la carrière mère.
    public string GroupePropre { get; init; } = "";
    public string StatutPropre { get; init; } = "";
    public string DescriptionPropre { get; init; } = "";
    public CitationDto[] AmbiancePropre { get; init; } = [];
    public string RestrictionPropre { get; init; } = "";
    public string LeitmotivPropre { get; init; } = "";
    public AptitudeDto? CompetenceDeMetierPropre { get; init; }

    // Valeurs affichées : celles de la carrière, sinon celles de sa mère (et ainsi de suite).
    public string Groupe => GroupePropre != "" ? GroupePropre : Parent?.Groupe ?? "";
    public string Statut => StatutPropre != "" ? StatutPropre : Parent?.Statut ?? "";
    public CitationDto[] Ambiance => AmbiancePropre.Length > 0 ? AmbiancePropre : Parent?.Ambiance ?? [];
    public string Restriction => RestrictionPropre != "" ? RestrictionPropre : Parent?.Restriction ?? "";
    public string Leitmotiv => LeitmotivPropre != "" ? LeitmotivPropre : Parent?.Leitmotiv ?? "";
    public AptitudeDto? CompetenceDeMetier => CompetenceDeMetierPropre ?? Parent?.CompetenceDeMetier;

    /// <summary>
    /// Description de la carrière, sinon de sa mère ; à défaut, pour une carrière avancée,
    /// celle de la première filière qui y mène et qui en a une.
    /// </summary>
    public string Description
    {
        get
        {
            var description = DescriptionDeLaLignee;
            return description != ""
                ? description
                : FilieresDAvancement.Select(f => f.DescriptionDeLaLignee).FirstOrDefault(d => d != "") ?? "";
        }
    }

    private string DescriptionDeLaLignee => DescriptionPropre != "" ? DescriptionPropre : Parent?.DescriptionDeLaLignee ?? "";

    public CarriereDto? Parent;
    public IReadOnlyList<CarriereDto> SousElements = [];

    public IReadOnlyList<CarriereDto> Debouches = [];
    public IReadOnlyList<CarriereDto> Filieres = [];

    public IReadOnlyList<CarriereDto> FilieresDAvancement = [];
    public IReadOnlyList<CarriereDto> DebouchesDAvancements = [];

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

    public IReadOnlyList<AptitudeDto> Competences { get; } = Aptitudes.Where(a => a.EstUneCompetence).OrderBy(a => a.Nom).ToArray();
    public IReadOnlyList<AptitudeDto> Talents { get; } = Aptitudes.Where(a => a.EstUnTalent).OrderBy(a => a.Nom).ToArray();
    public IReadOnlyList<AptitudeDto> Traits { get; } = Aptitudes.Where(a => a.EstUnTrait).OrderBy(a => a.Nom).ToArray();

    public IReadOnlyList<IReadOnlyList<AptitudeDto>> ChoixCompetences { get; } = AptitudesChoix.Where(choix => choix.Count > 0 && choix[0].EstUneCompetence).ToArray();
    public IReadOnlyList<IReadOnlyList<AptitudeDto>> ChoixTalents { get; } = AptitudesChoix.Where(choix => choix.Count > 0 && choix[0].EstUnTalent).ToArray();
    public IReadOnlyList<IReadOnlyList<AptitudeDto>> ChoixTraits { get; } = AptitudesChoix.Where(choix => choix.Count > 0 && choix[0].EstUnTrait).ToArray();
    
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

    public IReadOnlyList<AptitudeDto> AptitudesPourScore { get; } = Aptitudes.Union(AptitudesChoix.SelectMany(c => c)).ToArray();

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
