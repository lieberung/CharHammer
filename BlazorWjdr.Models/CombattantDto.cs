using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Models;

public class CombattantDto
{
    public string Code => $"{Combattant.Id}#{Nom}";

    public BestioleDto Combattant { get; init; } = null!;
    public string Nom { get; init; } = null!;
    public bool Ennemi { get; init; }

    public int JetDInitiative { get; set; }
    public string DetailDuJet { get; set; } = "";
    public CombattantDto? EngageContre { get; set; }

    public IEnumerable<AptitudeAcquiseDto> CompetencesMartiales => Combattant.AptitudesAcquises
        .Where(aa => aa.Aptitude is { Martial: true, EstUneCompetence: true }).ToArray();
    public AptitudeAcquiseDto[] AutresTraitsMartiaux => Combattant.AptitudesAcquises
        .Where(aa => aa.Aptitude is { Martial: true, EstUneCompetence: false }).ToArray();
}