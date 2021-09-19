namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class DieuxService
    {
        private readonly Dictionary<int, DieuDto> _cacheDieu;

        public DieuxService(Dictionary<int, DieuDto> dataDieux)
        {
            _cacheDieu = dataDieux;
        }

        public List<DieuDto> AllDieux =>_cacheDieu.Values.ToList();

        public DieuDto GetDieu(int id) => _cacheDieu[id];

        public string NomDuCulte(int idCulte)
        {
            var dieu = _cacheDieu.Values.First(d => d.Ordres.Any(o => o.Id == idCulte));
            var culte = dieu.Ordres.First(o => o.Id == idCulte);

            return culte.Nom.Contains(dieu.Nom) ? culte.Nom : $"{culte.Nom} ({dieu.Nom})";
        }
    }
}
