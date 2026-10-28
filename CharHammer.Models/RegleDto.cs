namespace CharHammer.Models;

public record RegleDto(
    int Id,
    bool Html,
    IReadOnlyList<int> ReglesId,
    string Titre,
    string Regle,
    IReadOnlyList<CarriereDto> Carrieres,
    IReadOnlyList<BestioleDto> Bestioles,
    IReadOnlyList<TableDto> Tables,
    //IReadOnlyList<LieuDto> Lieux,
    IReadOnlyList<AptitudeDto> Aptitudes,
    IReadOnlyList<IReadOnlyList<AptitudeDto>> AptitudesChoix,
    IReadOnlyList<LieuDto> Lieux)
{
    public IReadOnlyList<RegleDto> SousRegles = [];

    public IReadOnlyList<AptitudeDto> Competences { get; } = Aptitudes.Where(a => a.EstUneCompetence).ToArray();
    public IReadOnlyList<AptitudeDto> Talents { get; } = Aptitudes.Where(a => a.EstUnTalent).ToArray();
    public IReadOnlyList<AptitudeDto> Traits { get; } = Aptitudes.Where(a => a.EstUnTrait).ToArray();

    public IReadOnlyList<IReadOnlyList<AptitudeDto>> ChoixCompetences { get; } = AptitudesChoix.Where(choix => choix.Count > 0 && choix[0].EstUneCompetence).ToArray();
    public IReadOnlyList<IReadOnlyList<AptitudeDto>> ChoixTalents { get; } = AptitudesChoix.Where(choix => choix.Count > 0 && choix[0].EstUnTalent).ToArray();
    public IReadOnlyList<IReadOnlyList<AptitudeDto>> ChoixTraits { get; } = AptitudesChoix.Where(choix => choix.Count > 0 && choix[0].EstUnTrait).ToArray();

    public bool ProposeAuMoinsUnTalent => Talents.Any() || ChoixTalents.Any();
    public bool ProposeAuMoinsUnTrait => Traits.Any() || ChoixTraits.Any();
}