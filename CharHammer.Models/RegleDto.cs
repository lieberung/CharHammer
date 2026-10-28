namespace CharHammer.Models;

public class RegleDto(
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
    public int Id { get; } = Id;
    public bool Html { get; } = Html;
    public IReadOnlyList<int> ReglesId { get; } = ReglesId;
    public string Titre { get; } = Titre;
    public string Regle { get; } = Regle;
    public IReadOnlyList<CarriereDto> Carrieres { get; } = Carrieres;
    public IReadOnlyList<BestioleDto> Bestioles { get; } = Bestioles;
    public IReadOnlyList<TableDto> Tables { get; } = Tables;
    public IReadOnlyList<AptitudeDto> Aptitudes { get; } = Aptitudes;
    public IReadOnlyList<IReadOnlyList<AptitudeDto>> AptitudesChoix { get; } = AptitudesChoix;
    public IReadOnlyList<LieuDto> Lieux { get; } = Lieux;

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