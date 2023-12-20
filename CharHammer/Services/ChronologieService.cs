namespace CharHammer.Services;

public class ChronologieService(IEnumerable<ChronologieDto> data, IReadOnlyDictionary<int, DomaineDto> domaines)
{
    public IEnumerable<ChronologieDto> Chronologie { get; } = data.ToArray();
    public IEnumerable<DomaineDto> Domaines { get; } = domaines.Values.OrderBy(d => d.Nom).ToArray();
}
