namespace CharHammer.Services;

using Models;
using System.Collections.Generic;

public class ReglesService(IReadOnlyDictionary<int, RegleDto> data)
{
    public IEnumerable<RegleDto> AllRegles { get; } = data.Values.ToArray();
    public RegleDto GetRegle(int id) => data[id];
}