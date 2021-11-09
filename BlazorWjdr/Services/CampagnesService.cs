using BlazorWjdr.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Services
{
    public class CampagnesService
    {
        private readonly IEnumerable<CampagneDto> _campagnes;

        public CampagnesService(IEnumerable<CampagneDto> campagnes)
        {
            _campagnes = campagnes;
        }

        public IEnumerable<CampagneDto> AllCampagnes() => _campagnes;

        public SeanceDto GetSeance(int campagneId, string date)
        {
            return _campagnes.First(c => c.Id == campagneId).Seances.First(s => s.Quand == date);
        }

        public IEnumerable<CampagneDto> CampagnesAuxquellesAParticipe(BestioleDto pj)
            => _campagnes
                .Where(c => c.Seances.Any(s => s.Pjs.Contains(pj)))
                .OrderBy(c => c.Titre);
    }
}
