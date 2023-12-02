namespace BlazorWjdr.Services;

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

    public RaceDto Elfes => GetRace(IdElfes);
    public RaceDto ElfesSylvains => GetRace(IdElfesSylvains);
    public RaceDto HautsElfes => GetRace(IdHautsElfes);
    public RaceDto Humains => GetRace(IdHumains);
    public RaceDto HumainsImperiaux => GetRace(IdHumainsImperiaux);
    public RaceDto HumainsMutants => GetRace(IdHumainsMutants);
    public RaceDto Halflings => GetRace(IdHalflings);
    public RaceDto Nains => GetRace(IdNains);
    public RaceDto Gnomes => GetRace(IdGnomes);

    public static int IdElfes => 25;
    public static int IdElfesSylvains => 65;
    public static int IdHautsElfes => 66;
    public static int IdHumains => 1;
    public static int IdHumainsImperiaux => 2;
    public static int IdHumainsMutants => 64;
    public static int IdHalflings => 26;
    public static int IdNains => 27;
    public static int IdGnomes => 63;
}
