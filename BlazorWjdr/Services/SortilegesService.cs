namespace BlazorWjdr.Services;

using Models;
using System.Collections.Generic;

public class SortilegesService
{
    private readonly Dictionary<int, SortilegeDto> _cacheSortileges;

    public SortilegesService(Dictionary<int, SortilegeDto> sortileges)
    {
        _cacheSortileges = sortileges;
    }

    public IEnumerable<SortilegeDto> AllSortileges => _cacheSortileges.Values;
    //public SortilegeDto GetSortilege(int id) => _cacheSortileges[id];
}
