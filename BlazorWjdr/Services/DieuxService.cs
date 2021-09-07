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
    }
}
