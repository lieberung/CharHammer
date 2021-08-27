using System.Diagnostics;
using BlazorWjdr.DataSource.JsonDto;

namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LieuxService
    {
        private readonly List<JsonLieuType> _dataLieuxTypes;
        private readonly List<JsonLieu> _dataLieux;
        private Dictionary<int, LieuTypeDto>? _cacheLieuType;
        private Dictionary<int, LieuDto>? _cacheLieu;

        public LieuxService(List<JsonLieuType> dataLieuxTypes, List<JsonLieu> dataLieux)
        {
            _dataLieux = dataLieux;
            _dataLieuxTypes = dataLieuxTypes;
        }
        
        protected Dictionary<int, LieuTypeDto> AllTypesDeLieu
        {
            get
            {
                if (_cacheLieuType == null)
                    Initialize();
                Debug.Assert(_cacheLieuType != null, nameof(_cacheLieuType) + " != null");
                return _cacheLieuType;
            }
        }
        
        public List<LieuDto> AllLieux
        {
            get
            {
                if (_cacheLieu == null)
                    Initialize();
                Debug.Assert(_cacheLieu != null, nameof(_cacheLieu) + " != null");
                return _cacheLieu.Values.ToList();
            }
        }

        public LieuTypeDto GetTypeDeLieu(int id)
        {
            if (_cacheLieuType == null)
                Initialize();
            Debug.Assert(_cacheLieuType != null, nameof(_cacheLieuType) + " != null");
            return _cacheLieuType[id];
        }

        public IEnumerable<LieuDto> GetLieux(IEnumerable<int> ids) => ids.Select(GetLieu).ToArray();
        public LieuDto GetLieu(int id)
        {
            if (_cacheLieu == null)
                Initialize();
            Debug.Assert(_cacheLieu != null, nameof(_cacheLieu) + " != null");
            return _cacheLieu[id];
        }

        private void Initialize()
        {
            _cacheLieuType = _dataLieuxTypes
                .Select(t => new LieuTypeDto
                {
                    Id = t.id,
                    Nom = t.libelle,
                    ParentId = t.parentid
                }).ToDictionary(k => k.Id, v => v);

            foreach (var lieuType in _cacheLieuType.Values.Where(t => t.ParentId.HasValue))
            {
                lieuType.Parent = _cacheLieuType[lieuType.ParentId!.Value];
            }
            
            _cacheLieu = _dataLieux
                .Select(l => new LieuDto
                {
                    Id = l.id,
                    Nom = l.nom,
                    Description = l.description ?? "",
                    ParentId = l.fk_parentid,
                    TypeDeLieu = _cacheLieuType[l.fk_typeid]
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var lieu in _cacheLieu.Values.Where(t => t.ParentId.HasValue))
            {
                lieu.Parent = _cacheLieu[lieu.ParentId!.Value];
            }

            foreach (var lieu in _cacheLieu.Values)
            {
                lieu.SousElements.AddRange(_cacheLieu.Values
                    .Where(c=>c.Parent == lieu)
                    .OrderBy(c => c.Nom));                
            }
        }
    }
}
