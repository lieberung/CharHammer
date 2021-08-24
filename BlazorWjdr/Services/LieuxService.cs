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
        private List<LieuDto>? _allLieux;

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
                if (_allLieux == null)
                    Initialize();
                Debug.Assert(_allLieux != null, nameof(_allLieux) + " != null");
                return _allLieux;
            }
        }

        public LieuTypeDto GetTypeDeLieu(int id)
        {
            if (_cacheLieuType == null)
                Initialize();
#pragma warning disable CS8602 // DeLieu of a possibly null Lieu.
            return _cacheLieuType[id];
#pragma warning restore CS8602 // DeLieu of a possibly null Lieu.
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
            
            _allLieux = _cacheLieu.Values.ToList();
            
            foreach (var lieu in _allLieux.Where(t => t.ParentId.HasValue))
            {
                lieu.Parent = _cacheLieu[lieu.ParentId!.Value];
            }

            foreach (var lieu in _allLieux)
            {
                lieu.SousElements.AddRange(_allLieux
                    .Where(c=>c.Parent == lieu)
                    .OrderBy(c => c.Nom));                
            }
        }
    }
}
