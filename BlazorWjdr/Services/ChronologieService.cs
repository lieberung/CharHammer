using BlazorWjdr.DataSource.JsonDto;
using BlazorWjdr.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWjdr.Services
{
    public class ChronologieService
    {
        private readonly List<JsonChrono> _dataChrono;
        private readonly ReferencesService _referencesService;
        private ChronologieDto[]? _chronologie;

        public ChronologieService(List<JsonChrono> dataChrono, ReferencesService referencesService)
        {
            _dataChrono = dataChrono;
            _referencesService = referencesService;
        }

        private void Initialize()
        {
            _chronologie = _dataChrono
                .Select(c => new ChronologieDto(
                    c.debut,
                    c.fin,
                    c.resume,
                    c.titre ?? "",
                    c.comment ?? "",
                    c.sources.Select(id => _referencesService.GetReference(id)).ToArray()
                ))
                .OrderBy(c => c.Debut).ThenBy(c => c.Fin)
                .ToArray();
            
            //_dataChrono.Clear();
        }

        public ChronologieDto[] AllChronologie()
        {
            if (_chronologie == null)
                Initialize();

            Debug.Assert(_chronologie != null, nameof(_chronologie) + " != null");
            return _chronologie;
        }
    }
}
