namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReferencesService
    {
        private Dictionary<int, ReferenceDto>? _cacheReference;
        private List<ReferenceDto>? _allReferences;

        private List<ReferenceDto> AllReferences
        {
            get
            {
                if (_allReferences == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null reference return.
                return _allReferences;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
        
        public Task<ReferenceDto[]> Items()
        {
            return Task.FromResult(AllReferences.ToArray());
        }

        public ReferenceDto GetReference(int id)
        {
            if (_cacheReference == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheReference[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private void Initialize()
        {
            _cacheReference = DataSource.JsonLoader
                .GetRootReference()
                .items
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
        }

        public ReferenceDto LivreLesChevaliersDuGraal => GetReference(15);
        public ReferenceDto LivreLeDucheDesDamnes => GetReference(16);
        public ReferenceDto LivreLaReineDesGlaces => GetReference(14);
        public ReferenceDto LivreLesFilsDuRatCornu => GetReference(17);
        public ReferenceDto LivreLeTomeDeLaRedemption => GetReference(13);
    }
}
