using CharHammer.Models;

namespace CharHammer.Services;

public class ChronologieService(IEnumerable<ChronologieDto> data)
{
    public IEnumerable<ChronologieDto> Chronologie { get; } = data.ToArray();
}
