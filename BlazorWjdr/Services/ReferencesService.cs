namespace BlazorWjdr.Services
{
    using BlazorWjdr.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReferencesService
    {
        private Dictionary<int, ReferenceDto>? _cacheReference = null;
        private List<ReferenceDto>? _allReferences = null;

        public ReferencesService()
        {
        }

        protected List<ReferenceDto> AllReferences
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
                    Id = c.referenceid,
                    AnneeDePublication = c.referencepublishyear,
                    Code = c.referencecode,
                    Titre = c.referencetitre,
                    Version = c.referenceversion
                })
                .ToDictionary(k => k.Id, v => v);
            _allReferences = _cacheReference.Values.ToList();
        }
    }
}
