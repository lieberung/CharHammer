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
    public List<AptitudeDto[]> AptitudesChoix { get; set; } = null!;
    
    public List<AptitudeDto> Competences => Aptitudes.Where(a => a.EstUneCompetence).ToList();
    public List<AptitudeDto> Talents => Aptitudes.Where(a => a.EstUnTalent).ToList();
    public List<AptitudeDto> Traits => Aptitudes.Where(a => a.EstUnTrait).ToList();

    public List<AptitudeDto[]> ChoixCompetences => AptitudesChoix.Where(choix => choix.First().EstUneCompetence).ToList();
    public List<AptitudeDto[]> ChoixTalents => AptitudesChoix.Where(choix => choix.First().EstUnTalent).ToList();
    public List<AptitudeDto[]> ChoixTraits => AptitudesChoix.Where(choix => choix.First().EstUnTrait).ToList();

    public bool ProposeAuMoinsUnTalent => Talents.Any() || ChoixTalents.Any();
    public bool ProposeAuMoinsUnTrait => Traits.Any() || ChoixTraits.Any();
}