namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BestiolesService
    {
        private readonly Dictionary<int, BestioleDto> _cacheBestiole;

        public BestiolesService(Dictionary<int, BestioleDto> data)
        {
            _cacheBestiole = data;
        }
        
        public IEnumerable<BestioleDto> AllBestioles => _cacheBestiole.Values.ToList();
        public BestioleDto GetBestiole(int id) => _cacheBestiole[id];
    }
}
