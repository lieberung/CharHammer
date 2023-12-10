using System.Collections.Generic;

namespace CharHammer.Models;

public record LieuTypeDto(int Id, string Nom, int? ParentId)
{
    public LieuTypeDto? Parent;
}

public record LieuDto(
    int Id, LieuTypeDto TypeDeLieu, int? ParentId, string Nom, string Population,
    string Allegeance, string Industrie, string Description, bool Ignorer
) : ISearchable
{
    public LieuDto? Parent;
    public List<LieuDto> SousElements = [];
    
    public string Image => $"lieux/{Id}.png";

    public int ParentsCount {
        get
        {
            if (Parent is null)
                return 0;
            return Parent.ParentsCount + 1;
        }
    }
}
