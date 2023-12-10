using System.Collections.Generic;
using System.Linq;

namespace CharHammer.Models;

public record RegleDto(
    int Id,
    bool Html,
    IEnumerable<int> ReglesId,
    string Titre,
    string Regle,
    IEnumerable<CarriereDto> Carrieres,
    IEnumerable<BestioleDto> Bestioles,
    IEnumerable<TableDto> Tables,
    //IEnumerable<LieuDto> Lieux,
    IEnumerable<AptitudeDto> Aptitudes,
    IEnumerable<IEnumerable<AptitudeDto>> AptitudesChoix)
{
    public IEnumerable<RegleDto> SousRegles = [];

    public IEnumerable<AptitudeDto> Competences => Aptitudes.Where(a => a.EstUneCompetence);
    public IEnumerable<AptitudeDto> Talents => Aptitudes.Where(a => a.EstUnTalent);
    public IEnumerable<AptitudeDto> Traits => Aptitudes.Where(a => a.EstUnTrait);

    public IEnumerable<IEnumerable<AptitudeDto>> ChoixCompetences => AptitudesChoix.Where(choix => choix.First().EstUneCompetence);
    public IEnumerable<IEnumerable<AptitudeDto>> ChoixTalents => AptitudesChoix.Where(choix => choix.First().EstUnTalent);
    public IEnumerable<IEnumerable<AptitudeDto>> ChoixTraits => AptitudesChoix.Where(choix => choix.First().EstUnTrait);

    public bool ProposeAuMoinsUnTalent => Talents.Any() || ChoixTalents.Any();
    public bool ProposeAuMoinsUnTrait => Traits.Any() || ChoixTraits.Any();
}