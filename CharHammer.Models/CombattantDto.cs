namespace CharHammer.Models;

public record CombattantDto(BestioleDto Combattant, string Nom, bool Ennemi)
{
    public string Code => $"{Combattant.Id}#{Nom}";

    public int JetDInitiative { get; set; }
    public string DetailDuJet { get; set; } = "";
    public CombattantDto? EngageContre { get; set; }

    public IEnumerable<AptitudeAcquiseDto> CompetencesMartiales => Combattant.AptitudesAcquises
        .Where(aa => aa.Aptitude is { Martial: true, EstUneCompetence: true });
    public IEnumerable<AptitudeAcquiseDto> AutresTraitsMartiaux => Combattant.AptitudesAcquises
        .Where(aa => aa.Aptitude is { Martial: true, EstUneCompetence: false });
}