namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class RacesService
    {
        private readonly Dictionary<int, RaceDto> _cacheRace;

        public RacesService(Dictionary<int, RaceDto> races)
        {
            _cacheRace = races;
        }

        public List<RaceDto> AllRaces => _cacheRace.Values.ToList();
        public RaceDto GetRace(int id) => _cacheRace[id];

        public RaceDto Elfes => GetRace(25);
        public RaceDto Humains => GetRace(1);
        public RaceDto HumainsMutants => GetRace(64);
        public RaceDto Halfelings => GetRace(26);
        public RaceDto Nains => GetRace(27);
        public RaceDto Gnomes => GetRace(63);
    }
}
