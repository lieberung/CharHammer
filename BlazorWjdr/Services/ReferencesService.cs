using System.Diagnostics;

namespace BlazorWjdr.Services
{
    using BlazorWjdr.DataSource.JsonDto;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReferencesService
    {
        private readonly List<JsonReference> _dataRefernces;
        private Dictionary<int, ReferenceDto>? _cacheReference;
        private List<ReferenceDto>? _allReferences;

        public ReferencesService(List<JsonReference> dataReferences)
        {
            _dataRefernces = dataReferences;
        }

        public List<ReferenceDto> AllReferences
        {
            get
            {
                if (_allReferences == null)
                    Initialize();
                Debug.Assert(_allReferences != null, nameof(_allReferences) + " != null");
                return _allReferences;
            }
        }

        public ReferenceDto GetReference(int id)
        {
            if (_cacheReference == null)
                Initialize();
            Debug.Assert(_cacheReference != null, nameof(_cacheReference) + " != null");
            return _cacheReference[id];
        }

        private void Initialize()
        {
            _cacheReference = _dataRefernces
                .Select(c => new ReferenceDto
                {
                    Id = c.id,
                    AnneeDePublication = c.publishyear ?? 6666,
                    Code = c.code,
                    Titre = c.titre,
                    Version = c.version
                })
                .ToDictionary(k => k.Id, v => v);

            _allReferences = _cacheReference.Values.ToList();

            //_dataRefernces.Clear();
        }

        public ReferenceDto LivreLesChevaliersDuGraal => GetReference(15);
        public ReferenceDto LivreLeDucheDesDamnes => GetReference(16);
        public ReferenceDto LivreLaReineDesGlaces => GetReference(14);
        public ReferenceDto LivreLesFilsDuRatCornu => GetReference(17);
        public ReferenceDto LivreLeTomeDeLaRedemption => GetReference(13);
    }
}
