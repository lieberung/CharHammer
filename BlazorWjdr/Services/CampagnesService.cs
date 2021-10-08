using BlazorWjdr.Models;
using System.Collections.Generic;

namespace BlazorWjdr.Services
{
    public class CampagnesService
    {
        private readonly IReadOnlyDictionary<int, UserDto> _users;
        private readonly IReadOnlyDictionary<int, TeamDto> _teams;
        private readonly IEnumerable<CampagneDto> _campagnes;

        public CampagnesService(
            IReadOnlyDictionary<int, UserDto> users,
            IReadOnlyDictionary<int, TeamDto> teams,
            IEnumerable<CampagneDto> campagnes)
        {
            _users = users;
            _teams = teams;
            _campagnes = campagnes;
        }

        public IEnumerable<CampagneDto> AllCampagnes()
        {
            return _campagnes;
        }
    }
}
