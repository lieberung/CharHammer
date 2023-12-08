namespace BlazorWjdr.Services;

using Models;
using System.Collections.Generic;

public class ReglesService
{
    private readonly Dictionary<int, RegleDto> _cacheRegle;

    public ReglesService(Dictionary<int, RegleDto> data)
    {
        _cacheRegle = data;
    }

    public IEnumerable<RegleDto> AllRegles => _cacheRegle.Values;
    public RegleDto GetRegle(int id) => _cacheRegle[id];
}