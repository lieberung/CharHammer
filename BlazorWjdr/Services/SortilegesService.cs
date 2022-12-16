namespace BlazorWjdr.Services;

using Models;
using System.Collections.Generic;
using System.Linq;

public class SortilegesService
{
    private readonly Dictionary<int, SortilegeDto> _cacheSortileges;

    public SortilegesService(Dictionary<int, SortilegeDto> sortileges)
    {
        _cacheSortileges = sortileges;
    }

    public List<SortilegeDto> AllSortileges => _cacheSortileges.Values.ToList();
    public SortilegeDto GetRace(int id) => _cacheSortileges[id];
}
