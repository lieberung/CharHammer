namespace CharHammer.Models;

using System.Linq;
using System.Collections.Generic;

public record AptitudeDto(
    int Id, bool Ignore, bool Martial, string Nom, string? NomEn, string? Spe,
    string Categ, string CategSpe, string? Max, int Severite, string Guerison,
    bool? Contagieux, int? AptitudeMereId,
    IEnumerable<int> AptitudesLieesIds, IEnumerable<int> IncompatiblesIds
) : ISearchable
{
    public IEnumerable<string> MotsClefDeRecherche = [];
    public string NomPourRecherche = "";

    public string CaracteristiqueAssociee = "";
    public string Description = "";
    public string Resume = "";
    public string Tests = "";

    public bool EstUneCompetence => Categ == "skill";
    public bool EstUneCompetenceDeBase => EstUneCompetence && CategSpe == "bas";
    public bool EstUnTalent => Categ == "talent";
    public bool EstUnTrait => Categ == "trait";
    public bool EstUnSigneDistinctif => EstUnTrait && CategSpe == "trait";

    public AptitudeDto? Parent { get; set; }
    public IEnumerable<AptitudeDto> SousElements = [];
    public List<AptitudeDto> AptitudesLiees { get; set; } = [];
    public List<AptitudeDto> Incompatibles { get; set; } = null!;
    public List<CarriereDto> CarrieresLiees { get; } = [];

    public IEnumerable<AptitudeDto> CompetencesLiees => AptitudesLiees.Where(a => a.EstUneCompetence);
    public IEnumerable<AptitudeDto> TalentsLies => AptitudesLiees.Where(a => a.EstUnTalent);
    public IEnumerable<AptitudeDto> TraitsLies => AptitudesLiees.Where(a => a.EstUnTrait);

    public string Icon => EstUneCompetence ? "target" : EstUnTalent ? "brush" : EstUnTrait ? "droplet" : "error";
    public string NomComplet => Nom + (string.IsNullOrWhiteSpace(Spe) ? "" : $" : {Spe}");
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

    public static IEnumerable<AptitudeAcquiseDto> GetList(IEnumerable<AptitudeDto> aptitudes, ProfilDto profil)
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
        return liste.OrderBy(c => c.Aptitude.NomComplet);
    }
}
