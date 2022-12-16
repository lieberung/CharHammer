namespace BlazorWjdr.Services;

using Models;
using System.Collections.Generic;

public class TablesService
{
    private readonly Dictionary<int, TableDto> _cacheTable;

    public TablesService(Dictionary<int, TableDto> dataTables)
    {
        _cacheTable = dataTables;
    }

    public TableDto GetTable(int id) => _cacheTable[id];
}
