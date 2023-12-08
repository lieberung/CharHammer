using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Models;

public class RegleDto
{
    public int Id { get; init; }
    public bool Html { get; init; }
    public string Titre { get; init; } = null!;
    public int[] ReglesId { get; init; } = null!;
    public RegleDto[] SousRegles { get; set; } = null!;
    public CarriereDto[] Carrieres { get; init; } = null!;
    public BestioleDto[] Bestioles { get; init; } = null!;
    public TableDto[] Tables { get; init; } = null!;
    public LieuDto[] Lieux { get; init; } = null!;
    public string Regle { get; init; } = null!;
    
    public List<AptitudeDto> Aptitudes { get; init; } = null!;
    public List<AptitudeDto[]> AptitudesChoix { get; init; } = null!;
    
    public IEnumerable<AptitudeDto> Competences => Aptitudes.Where(a => a.EstUneCompetence).ToList();
    public IEnumerable<AptitudeDto> Talents => Aptitudes.Where(a => a.EstUnTalent).ToList();
    public IEnumerable<AptitudeDto> Traits => Aptitudes.Where(a => a.EstUnTrait).ToList();

    public IEnumerable<AptitudeDto[]> ChoixCompetences => AptitudesChoix.Where(choix => choix.First().EstUneCompetence).ToList();
    public IEnumerable<AptitudeDto[]> ChoixTalents => AptitudesChoix.Where(choix => choix.First().EstUnTalent).ToList();
    public IEnumerable<AptitudeDto[]> ChoixTraits => AptitudesChoix.Where(choix => choix.First().EstUnTrait).ToList();

    public bool ProposeAuMoinsUnTalent => Talents.Any() || ChoixTalents.Any();
    public bool ProposeAuMoinsUnTrait => Traits.Any() || ChoixTraits.Any();
}