namespace CharHammer.Models;

public class LieuTypeDto(int Id, string Nom, int? ParentId)
{
    public int Id { get; } = Id;
    public string Nom { get; } = Nom;
    public int? ParentId { get; } = ParentId;

    public LieuTypeDto? Parent;
}

public class LieuDto(
    int Id, LieuTypeDto TypeDeLieu, int? ParentId, string Nom, string Population,
    string Allegeance, string Industrie, string Description, bool Ignorer
) : ISearchable
{
    public int Id { get; } = Id;
    public LieuTypeDto TypeDeLieu { get; } = TypeDeLieu;
    public int? ParentId { get; } = ParentId;
    public string Nom { get; } = Nom;
    public string Population { get; } = Population;
    public string Allegeance { get; } = Allegeance;
    public string Industrie { get; } = Industrie;
    public string Description { get; } = Description;
    public bool Ignorer { get; } = Ignorer;

    public LieuDto? Parent;
    public List<LieuDto> SousElements = [];

    public int ParentsCount {
        get
        {
            if (Parent is null)
                return 0;
            return Parent.ParentsCount + 1;
        }
    }
}
