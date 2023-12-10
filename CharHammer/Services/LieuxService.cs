namespace CharHammer.Services;

using Models;
using System.Collections.Generic;
using System.Linq;

public class LieuxService(IReadOnlyDictionary<int, LieuTypeDto> dataLieuxTypes, IReadOnlyDictionary<int, LieuDto> data)
{
    protected IReadOnlyDictionary<int, LieuTypeDto> AllTypesDeLieu => dataLieuxTypes;
    public IEnumerable<LieuDto> AllLieux { get; } = data.Values.OrderBy(l => l.Nom).ToArray();
    //public LieuTypeDto GetTypeDeLieu(int id) => dataLieuxTypes[id];

    //public IEnumerable<LieuDto> GetLieux(IEnumerable<int> ids) => ids.Select(GetLieu);
    public LieuDto GetLieu(int id) => data[id];
    
    public IEnumerable<LieuDto> Recherche(string searchText)
    {
        searchText = GenericService.NettoyerPourRecherche(searchText);
        return AllLieux
            .Where(c => GenericService.NettoyerPourRecherche(c.Nom).Contains(searchText));
    }
}
