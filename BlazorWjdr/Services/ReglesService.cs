namespace BlazorWjdr.Services;

using Models;
using System.Collections.Generic;
using System.Linq;

public class ReglesService
{
    private readonly Dictionary<int, RegleDto> _cacheRegle;

    public ReglesService(Dictionary<int, RegleDto> data)
    {
        _cacheRegle = data;
    }

    public List<RegleDto> AllRegles => _cacheRegle.Values.ToList();
    public RegleDto GetRegle(int id) => _cacheRegle[id];
}