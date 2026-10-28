namespace CharHammer.Models;

public record UserDto(int Id, string Email, string Pseudo);

public record TeamDto(int Id, string Nom);

public record CampagneDto(int Id, string Titre, UserDto Mj, TeamDto Team, IReadOnlyList<SeanceDto> Seances, IReadOnlyList<ContactDeCampagneDto> Contacts)
{
    public SeanceDto SeancePrecedente() => Seances.Where(s => s.Secret == false).OrderByDescending(s => s.Quand).First();
    public SeanceDto SeanceActuelle() => Seances.Where(s => s.Secret).OrderBy(s => s.Quand).First();
    
    public IEnumerable<SeanceDto> SeancesPourLActe(int acte, bool godMode) => Seances
            .Where(s => s.Acte == acte && (godMode || s.Secret == false))
            .OrderBy(s => s.Quand);
}

public record SeanceDto(string Quand, bool Secret, int Acte, string Debut, int Duree, string Titre,
    ScenarioDto? Scenario, int Xp, string? XpComment, string Resume,
    IReadOnlyList<LieuDto> Lieux, IReadOnlyList<BestioleDto> Pjs, IReadOnlyList<FactDto> Facts, IReadOnlyList<RencontreDto> Rencontres);

public record ContactDeCampagneDto(
    BestioleDto Pnj,
    LieuDto LieuDeRencontre,
    LieuDto? LieuDeResidence,
    IReadOnlyList<CarriereDto> ProposeLesCarrieres,
    IReadOnlyList<string> Notes,
    string Description);

public record FactDto(int Tri, IReadOnlyList<BestioleDto> Pjs, string Fact);

public record RencontreDto(string Groupe, IReadOnlyList<CombattantDto> Allies, IReadOnlyList<CombattantDto> Ennemis);