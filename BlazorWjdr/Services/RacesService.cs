namespace BlazorWjdr.Services;

using Models;
using System.Collections.Generic;
using System.Linq;

public class RacesService
{
    private readonly Dictionary<int, RaceDto> _cacheRace;
    private readonly Dictionary<int, RaceDto[]> _cacheCultures = new();

    public RacesService(Dictionary<int, RaceDto> races)
    {
        _cacheRace = races;
        _cacheCultures.Add(IdHumains, new [] { HumainsImperiaux, HumainsBretonniens, HumainsGospodars, HumainsUngols });
        _cacheCultures.Add(IdElfes, new [] { ElfesSylvains, HautsElfes });
    }

    public List<RaceDto> AllRaces => _cacheRace.Values.ToList();
    public RaceDto GetRace(int id) => _cacheRace[id];

    public RaceDto Elfes => GetRace(IdElfes);
    public RaceDto ElfesSylvains => GetRace(IdElfesSylvains);
    public RaceDto HautsElfes => GetRace(IdHautsElfes);
    public RaceDto Humains => GetRace(IdHumains);
    public RaceDto HumainsImperiaux => GetRace(IdHumainsImperiaux);
    public RaceDto HumainsBretonniens => GetRace(IdHumainsBretonniens);
    public RaceDto HumainsGospodars => GetRace(IdHumainsGospodars);
    public RaceDto HumainsUngols => GetRace(IdHumainsUngols);
    public RaceDto HumainsMutants => GetRace(IdHumainsMutants);
    public RaceDto Halflings => GetRace(IdHalflings);
    public RaceDto Nains => GetRace(IdNains);
    public RaceDto Gnomes => GetRace(IdGnomes);

    public const int IdElfes = 25;
    public const int IdElfesSylvains = 65;
    public const int IdHautsElfes = 66;
    public const int IdHumains = 1;
    public const int IdHumainsImperiaux = 2;
    public const int IdHumainsBretonniens = 4;
    public const int IdHumainsGospodars = 22;
    public const int IdHumainsUngols = 23;
    public const int IdHumainsMutants = 64;
    public const int IdHalflings = 26;
    public const int IdNains = 27;
    public const int IdGnomes = 63;
}
