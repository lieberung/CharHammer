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

        public DieuxService(List<JsonDieu> dataDieux)
        {
            _dataDieux = dataDieux;
        }

        public List<DieuDto> AllDieux
        {
            get
            {
                if (_cacheDieu == null)
                    Initialize();
                Debug.Assert(_cacheDieu != null, nameof(_cacheDieu) + " != null");
                return _cacheDieu.Values.ToList();
            }
        }

        public DieuDto GetDieu(int id)
        {
            if (_cacheDieu == null)
                Initialize();
            Debug.Assert(_cacheDieu != null, nameof(_cacheDieu) + " != null");
            return _cacheDieu[id];
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

            foreach (var dieu in _cacheDieu.Values.Where(d => d.PatronId.HasValue))
            {
                dieu.Patron = _cacheDieu[dieu.PatronId!.Value];
            }
        }
    }
}
