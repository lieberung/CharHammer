using BlazorWjdr.DomainModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWjdr.Services
{
    public class ChronologieService
    {
        public Task<ChronologieDto[]> GetLines()
        {
            return Task.FromResult(
                DataSource.JsonLoader.GetRootChrono()
                    .items
                    .Select(c => new ChronologieDto(c.debut, c.fin, c.resume, c.titre, c.comment ?? ""))
                    .OrderBy(c => c.Debut).ThenBy(c => c.Fin)
                    .ToArray()
            );
        }
    }
}
