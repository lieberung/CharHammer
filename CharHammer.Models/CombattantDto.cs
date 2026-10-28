namespace CharHammer.Models;

public record CombattantDto(BestioleDto Combattant, string Nom, bool Ennemi)
{
    public string Code => $"{Combattant.Id}#{Nom}";

    public int JetDInitiative { get; set; }
    public string DetailDuJet { get; set; } = "";
    public CombattantDto? EngageContre { get; set; }

    public IReadOnlyList<AptitudeAcquiseDto> CompetencesMartiales { get; } = Combattant.AptitudesAcquises
        .Where(aa => aa.Aptitude is { Martial: true, EstUneCompetence: true }).ToArray();
    public IReadOnlyList<AptitudeAcquiseDto> AutresTraitsMartiaux { get; } = Combattant.AptitudesAcquises
        .Where(aa => aa.Aptitude is { Martial: true, EstUneCompetence: false }).ToArray();
}