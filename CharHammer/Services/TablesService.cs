namespace CharHammer.Services;

public class TablesService(IReadOnlyDictionary<int, TableDto> dataTables)
{
    public TableDto GetTable(int id) => dataTables[id];
}
