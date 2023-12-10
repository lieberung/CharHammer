namespace CharHammer.Services;

using Models;
using System.Collections.Generic;

public class TablesService(IReadOnlyDictionary<int, TableDto> dataTables)
{
    public TableDto GetTable(int id) => dataTables[id];
}
