namespace CharHammer.Models;

public class AptitudeDto(
    int Id, bool Ignore, bool Martial, string Nom, string? NomEn, string? Spe,
    string Categ, string CategSpe, string? Max, int Severite, string Guerison,
    bool? Contagieux, int? AptitudeMereId,
    IReadOnlyList<int> AptitudesLieesIds, IReadOnlyList<int> IncompatiblesIds
) : ISearchable
{
    public int Id { get; } = Id;
    public bool Ignore { get; } = Ignore;
    public bool Martial { get; } = Martial;
    public string Nom { get; } = Nom;
    public string? NomEn { get; } = NomEn;
    public string? Spe { get; } = Spe;
    public string Categ { get; } = Categ;
    public string CategSpe { get; } = CategSpe;
    public string? Max { get; } = Max;
    public int Severite { get; } = Severite;
    public string Guerison { get; } = Guerison;
    public bool? Contagieux { get; } = Contagieux;
    public int? AptitudeMereId { get; } = AptitudeMereId;
    public IReadOnlyList<int> AptitudesLieesIds { get; } = AptitudesLieesIds;
    public IReadOnlyList<int> IncompatiblesIds { get; } = IncompatiblesIds;

    public IReadOnlyList<string> MotsClefDeRecherche = [];
    public string NomPourRecherche = "";

    // Valeurs saisies dans aptitude.json. Vides quand elles doivent venir de l'aptitude mère.
    public string CaracteristiqueAssocieePropre { get; init; } = "";
    public string DescriptionPropre { get; init; } = "";
    public string ResumePropre { get; init; } = "";
    public string TestsPropres { get; init; } = "";

    // Valeurs affichées : celles de l'aptitude, sinon celles de sa mère (spécialisation sans texte propre).
    public string CaracteristiqueAssociee => CaracteristiqueAssocieePropre != "" ? CaracteristiqueAssocieePropre : Parent?.CaracteristiqueAssociee ?? "";
    public string Description => DescriptionPropre != "" ? DescriptionPropre : Parent?.Description ?? "";
    public string Resume => ResumePropre != "" ? ResumePropre : Parent?.Resume ?? "";
    public string Tests => TestsPropres != "" ? TestsPropres : Parent?.Tests ?? "";

    public bool EstUneCompetence => Categ == "skill";
    public bool EstUneCompetenceDeBase => EstUneCompetence && CategSpe == "bas";
    public bool EstUnTalent => Categ == "talent";
    public bool EstUnTrait => Categ == "trait";
    public bool EstUnSigneDistinctif => EstUnTrait && CategSpe == "trait";

    public AptitudeDto? Parent { get; set; }
    public IReadOnlyList<AptitudeDto> SousElements = [];
    public List<AptitudeDto> AptitudesLiees { get; set; } = [];
    public List<AptitudeDto> Incompatibles { get; set; } = null!;
    public List<CarriereDto> CarrieresLiees { get; } = [];

    public IReadOnlyList<AptitudeDto> CompetencesLiees { get; private set; } = [];
    public IReadOnlyList<AptitudeDto> TalentsLies { get; private set; } = [];
    public IReadOnlyList<AptitudeDto> TraitsLies { get; private set; } = [];

    /// <summary>Répartit <see cref="AptitudesLiees"/> par catégorie. À appeler une fois la liste définitivement remplie.</summary>
    public void FigerAptitudesLiees()
    {
        CompetencesLiees = AptitudesLiees.Where(a => a.EstUneCompetence).ToArray();
        TalentsLies = AptitudesLiees.Where(a => a.EstUnTalent).ToArray();
        TraitsLies = AptitudesLiees.Where(a => a.EstUnTrait).ToArray();
    }

    public string Icon => EstUneCompetence ? "bullseye" : EstUnTalent ? "brush" : EstUnTrait ? "droplet" : "x-circle";
    public string CategSpeSexy
    {
        get
        {
            if (string.IsNullOrWhiteSpace(CategSpe))
                return "non classé";
            return CategSpe switch
            {
                "trait" => "signe distinctif",
                "nevrose" => "névrose",
                "capacite" => "capacité",
                "creature" => "créature",
                "nature_avantageuse" => "prédisposition",
                "resistance" => "résistance",
                "animosite" => "animosité",
                _ => CategSpe
            };
        }
    }
}

public class AptitudeAcquiseDto
{
    private AptitudeAcquiseDto(AptitudeDto aptitude, int niveau)
    {
        Aptitude = aptitude;
        Niveau = niveau;
    }

    public AptitudeDto Aptitude { get; }
    public int Niveau { get; private set; }
    private int ChancesDeSucces { get; set; }

    public string Detail(bool afficherBonusDeCompetence)
    {
        if (Aptitude.EstUneCompetence)
        {
            return afficherBonusDeCompetence ? $"{Aptitude.Nom} (+{Niveau * 5}%)" : $"{Aptitude.Nom} ({ChancesDeSucces}%)";
        }
        if (Aptitude.EstUnTalent)
        {
            var rating = Niveau == 1 ? "" : $" ({Niveau})";
            return $"{Aptitude.Nom}{rating}";
        }
        var niveau = Niveau == 1 ? "" : $" (+{Niveau})";
        return $"{Aptitude.Nom}{niveau}";
    }

    public static IReadOnlyList<AptitudeAcquiseDto> GetList(IEnumerable<AptitudeDto> aptitudes, ProfilDto profil)
    {
        var liste = new List<AptitudeAcquiseDto>();
        foreach (var apt in aptitudes)
        {
            var ca = liste.FirstOrDefault(c => c.Aptitude == apt);
            if (ca is null)
                liste.Add(new AptitudeAcquiseDto(apt, 1));
            else
                ca.Niveau += 1;
        }

        foreach (var aptAcq in liste.Where(aa => aa.Aptitude.EstUneCompetence))
        {
            aptAcq.ChancesDeSucces = profil.GetStat(aptAcq.Aptitude.CaracteristiqueAssociee) + aptAcq.Niveau * 5;
        }
        return liste.OrderBy(c => c.Aptitude.Nom).ToArray();
    }
}
