namespace BlazorWjdr.Services
{
    using BlazorWjdr.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DieuxService
    {
        private Dictionary<int, DieuDto>? _cacheDieu = null;
        private List<DieuDto>? _allDieux = null;

        public DieuxService()
        {
        }

        protected List<DieuDto> AllDieux
        {
            get
            {
                if (_allDieux == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null Dieu return.
                return _allDieux;
#pragma warning restore CS8603 // Possible null Dieu return.
            }
        }
        
        public Task<DieuDto[]> Items()
        {
            return Task.FromResult(AllDieux.ToArray());
        }

        public DieuDto GetDieu(int id)
        {
            if (_cacheDieu == null)
                Initialize();
#pragma warning disable CS8602 // DeDieu of a possibly null Dieu.
            return _cacheDieu[id];
#pragma warning restore CS8602 // DeDieu of a possibly null Dieu.
        }

        private void Initialize()
        {
            _cacheDieu = DataSource.JsonLoader
                .GetRootDieu()
                .items
                .Select(c => new DieuDto
                {
                    Id = c.id,
                    Nom = c.nom,
                    Commentaire = c.comment,
                    Domaines = c.domaines,
                    Fideles = c.fideles,
                    Histoire = c.histoire,
                    Symboles = c.symboles,
                    SymbolesImages = c.symboles_image
                })
                .ToDictionary(k => k.Id, v => v);
            _allDieux = _cacheDieu.Values.ToList();
            foreach (var dieu in _allDieux.Where(d => d.PatronId.HasValue))
            {
                dieu.Patron = _cacheDieu[dieu.PatronId!.Value];
            }
        }
    }
}
