namespace CharHammer.Services;

public class ReferencesService(IReadOnlyDictionary<int, ReferenceDto> dataReferences)
{
    public IEnumerable<ReferenceDto> AllReferences { get; } = dataReferences.Values.OrderBy(r => r.AnneeDePublication).ToArray();
}
