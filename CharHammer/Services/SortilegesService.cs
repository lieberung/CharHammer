namespace CharHammer.Services;

public class SortilegesService(IReadOnlyDictionary<int, SortilegeDto> sortileges)
{
    public IEnumerable<SortilegeDto> AllSortileges { get; } = sortileges.Values.ToArray();
    //public SortilegeDto GetSortilege(int id) => sortileges[id];
}
