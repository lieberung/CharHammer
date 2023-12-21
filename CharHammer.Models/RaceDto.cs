namespace CharHammer.Models;

public record RaceDto(
    int Id, bool PourPj, bool GroupOnly, string NomMasculin, string NomFeminin, string Autochtones, string Description, int? ParentId,
    IEnumerable<AptitudeDto> Aptitudes, IEnumerable<LieuDto> Lieux, IEnumerable<OpinionDto> Opinions, IEnumerable<RaceInfoDto> Infos)
{
  public RaceDto? Parent;
  public IEnumerable<RaceDto> SousElements = [];

  public int ParentsCount => Parent is null ? 0 : Parent.ParentsCount + 1;
}

public record OpinionDto(int RaceId, string Ambiance)
{
  public RaceDto? Race;
}

public record RaceInfoDto(string? Titre, string Detail);
