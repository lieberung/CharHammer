using BlazorWjdr.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Services
{
    public class CampagnesService
    {
        // private readonly IReadOnlyDictionary<int, UserDto> _users;
        // private readonly IReadOnlyDictionary<int, TeamDto> _teams;
        private readonly IEnumerable<CampagneDto> _campagnes;

        public CampagnesService(
            // IReadOnlyDictionary<int, UserDto> users,
            // IReadOnlyDictionary<int, TeamDto> teams,
            IEnumerable<CampagneDto> campagnes)
        {
            // _users = users;
            // _teams = teams;
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
