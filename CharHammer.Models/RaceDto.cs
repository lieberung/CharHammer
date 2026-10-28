namespace CharHammer.Models;

public class RaceDto(
    int Id, bool PourPj, bool GroupOnly, string NomMasculin, string NomFeminin, string Autochtones, string Description, int? ParentId,
    IReadOnlyList<AptitudeDto> Aptitudes, IReadOnlyList<LieuDto> Lieux, IReadOnlyList<OpinionDto> Opinions, IReadOnlyList<RaceInfoDto> Infos)
{
    public int Id { get; } = Id;
    public bool PourPj { get; } = PourPj;
    public bool GroupOnly { get; } = GroupOnly;
    public string NomMasculin { get; } = NomMasculin;
    public string NomFeminin { get; } = NomFeminin;
    public string Autochtones { get; } = Autochtones;
    public string Description { get; } = Description;
    public int? ParentId { get; } = ParentId;
    public IReadOnlyList<AptitudeDto> Aptitudes { get; } = Aptitudes;
    public IReadOnlyList<LieuDto> Lieux { get; } = Lieux;
    public IReadOnlyList<OpinionDto> Opinions { get; } = Opinions;
    public IReadOnlyList<RaceInfoDto> Infos { get; } = Infos;

  public RaceDto? Parent;
  public IReadOnlyList<RaceDto> SousElements = [];

  public int ParentsCount => Parent is null ? 0 : Parent.ParentsCount + 1;
}

public class OpinionDto(int RaceId, string Ambiance)
{
    public int RaceId { get; } = RaceId;
    public string Ambiance { get; } = Ambiance;

  public RaceDto? Race;
}

public record RaceInfoDto(string? Titre, string Detail);
