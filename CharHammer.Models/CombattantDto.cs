namespace CharHammer.Models;

public class CombattantDto(BestioleDto Combattant, string Nom, bool Ennemi)
{
    public BestioleDto Combattant { get; } = Combattant;
    public string Nom { get; } = Nom;
    public bool Ennemi { get; } = Ennemi;

    public string Code => $"{Combattant.Id}#{Nom}";

    public IReadOnlyList<AptitudeAcquiseDto> CompetencesMartiales { get; } = Combattant.AptitudesAcquises
        .Where(aa => aa.Aptitude is { Martial: true, EstUneCompetence: true }).ToArray();
    public IReadOnlyList<AptitudeAcquiseDto> AutresTraitsMartiaux { get; } = Combattant.AptitudesAcquises
        .Where(aa => aa.Aptitude is { Martial: true, EstUneCompetence: false }).ToArray();
}