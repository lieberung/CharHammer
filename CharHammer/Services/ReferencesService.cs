namespace CharHammer.Services;

using Models;
using System.Collections.Generic;

public class ReferencesService(IReadOnlyDictionary<int, ReferenceDto> dataReferences)
{
    public IEnumerable<ReferenceDto> AllReferences { get; } = dataReferences.Values.OrderBy(r => r.AnneeDePublication).ToArray();
}
