namespace CharHammer.Services;

public class LieuxService(IReadOnlyDictionary<int, LieuTypeDto> dataLieuxTypes, IReadOnlyDictionary<int, LieuDto> data)
{
    protected IReadOnlyDictionary<int, LieuTypeDto> AllTypesDeLieu => dataLieuxTypes;
    public IEnumerable<LieuDto> AllLieux { get; } = data.Values.OrderBy(l => l.Nom).ToArray();

    private readonly (LieuDto Lieu, string Nom)[] _nomsPourRecherche = data.Values
        .OrderBy(l => l.Nom)
        .Select(l => (l, GenericService.NettoyerPourRecherche(l.Nom)))
        .ToArray();
    //public LieuTypeDto GetTypeDeLieu(int id) => dataLieuxTypes[id];

    //public IEnumerable<LieuDto> GetLieux(IEnumerable<int> ids) => ids.Select(GetLieu);
    public LieuDto GetLieu(int id) => data[id];
    
    public IEnumerable<LieuDto> Recherche(string searchText)
    {
        searchText = GenericService.NettoyerPourRecherche(searchText);
        return _nomsPourRecherche
            .Where(x => x.Nom.Contains(searchText))
            .Select(x => x.Lieu);
    }
}
