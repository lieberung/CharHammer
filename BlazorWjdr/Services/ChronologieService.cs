using BlazorWjdr.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWjdr.Services
{
    public class ChronologieService
    {
        private readonly ReferencesService _referencesService;
        public ChronologieService(ReferencesService referencesService)
        {
            _referencesService = referencesService;
        }
        
        public Task<ChronologieDto[]> GetLines()
        {
            return Task.FromResult(
                DataSource.JsonLoader.GetRootChrono()
                    .items
                    .Select(c => new ChronologieDto(
                        c.debut,
                        c.fin,
                        c.resume,
                        c.titre ?? "", 
                        c.comment ?? "",
                        c.sources.Select(id => _referencesService.GetReference(id)).ToArray()
                    ))
                    .OrderBy(c => c.Debut).ThenBy(c => c.Fin)
                    .ToArray()
            );
        }
    }
}
