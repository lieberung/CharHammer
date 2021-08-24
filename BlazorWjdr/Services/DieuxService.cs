using System.Diagnostics;

namespace BlazorWjdr.Services
{
    using BlazorWjdr.DataSource.JsonDto;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DieuxService
    {
        private readonly List<JsonDieu> _dataDieux;
        private Dictionary<int, DieuDto>? _cacheDieu;
        private List<DieuDto>? _allDieux;

        public DieuxService(List<JsonDieu> dataDieux)
        {
            _dataDieux = dataDieux;
        }

        public List<DieuDto> AllDieux
        {
            get
            {
                if (_allDieux == null)
                    Initialize();
                Debug.Assert(_allDieux != null, nameof(_allDieux) + " != null");
                return _allDieux;
            }
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
            _cacheDieu = _dataDieux
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
