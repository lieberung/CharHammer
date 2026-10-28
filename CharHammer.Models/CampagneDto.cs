namespace CharHammer.Models;

public class UserDto(int Id, string Email, string Pseudo)
{
    public int Id { get; } = Id;
    public string Email { get; } = Email;
    public string Pseudo { get; } = Pseudo;
}

public class TeamDto(int Id, string Nom)
{
    public int Id { get; } = Id;
    public string Nom { get; } = Nom;
}

public class CampagneDto(int Id, string Titre, UserDto Mj, TeamDto Team, IReadOnlyList<SeanceDto> Seances, IReadOnlyList<ContactDeCampagneDto> Contacts)
{
    public int Id { get; } = Id;
    public string Titre { get; } = Titre;
    public UserDto Mj { get; } = Mj;
    public TeamDto Team { get; } = Team;
    public IReadOnlyList<SeanceDto> Seances { get; } = Seances;
    public IReadOnlyList<ContactDeCampagneDto> Contacts { get; } = Contacts;

    /// <summary>Dernière séance jouée (publique), ou <c>null</c> si aucune.</summary>
    public SeanceDto? SeancePrecedente() => Seances.Where(s => s.Secret == false).OrderByDescending(s => s.Quand).FirstOrDefault();
    /// <summary>Prochaine séance (secrète, en préparation), ou <c>null</c> si aucune.</summary>
    public SeanceDto? SeanceActuelle() => Seances.Where(s => s.Secret).OrderBy(s => s.Quand).FirstOrDefault();
    
    public IEnumerable<SeanceDto> SeancesPourLActe(int acte, bool godMode) => Seances
            .Where(s => s.Acte == acte && (godMode || s.Secret == false))
            .OrderBy(s => s.Quand);
}

public class SeanceDto(string Quand, bool Secret, int Acte, string Debut, int Duree, string Titre,
    ScenarioDto? Scenario, int Xp, string? XpComment, string Resume,
    IReadOnlyList<LieuDto> Lieux, IReadOnlyList<BestioleDto> Pjs, IReadOnlyList<FactDto> Facts, IReadOnlyList<RencontreDto> Rencontres)
{
    public string Quand { get; } = Quand;
    public bool Secret { get; } = Secret;
    public int Acte { get; } = Acte;
    public string Debut { get; } = Debut;
    public int Duree { get; } = Duree;
    public string Titre { get; } = Titre;
    public ScenarioDto? Scenario { get; } = Scenario;
    public int Xp { get; } = Xp;
    public string? XpComment { get; } = XpComment;
    public string Resume { get; } = Resume;
    public IReadOnlyList<LieuDto> Lieux { get; } = Lieux;
    public IReadOnlyList<BestioleDto> Pjs { get; } = Pjs;
    public IReadOnlyList<FactDto> Facts { get; } = Facts;
    public IReadOnlyList<RencontreDto> Rencontres { get; } = Rencontres;
}

public class ContactDeCampagneDto(
    BestioleDto Pnj,
    LieuDto LieuDeRencontre,
    LieuDto? LieuDeResidence,
    IReadOnlyList<CarriereDto> ProposeLesCarrieres,
    IReadOnlyList<string> Notes,
    string Description)
{
    public BestioleDto Pnj { get; } = Pnj;
    public LieuDto LieuDeRencontre { get; } = LieuDeRencontre;
    public LieuDto? LieuDeResidence { get; } = LieuDeResidence;
    public IReadOnlyList<CarriereDto> ProposeLesCarrieres { get; } = ProposeLesCarrieres;
    public IReadOnlyList<string> Notes { get; } = Notes;
    public string Description { get; } = Description;
}

public record FactDto(int Tri, IReadOnlyList<BestioleDto> Pjs, string Fact);

public class RencontreDto(string Groupe, IReadOnlyList<CombattantDto> Allies, IReadOnlyList<CombattantDto> Ennemis)
{
    public string Groupe { get; } = Groupe;
    public IReadOnlyList<CombattantDto> Allies { get; } = Allies;
    public IReadOnlyList<CombattantDto> Ennemis { get; } = Ennemis;
}