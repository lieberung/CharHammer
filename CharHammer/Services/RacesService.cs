namespace CharHammer.Services;

using Models;
using System.Collections.Generic;

public class RacesService(IReadOnlyDictionary<int, RaceDto> data)
{
    public IEnumerable<RaceDto> AllRaces { get; } = data.Values.ToArray();
    public RaceDto GetRace(int id) => data[id];

    public RaceDto Elfes => GetRace(IdElfes);
    public RaceDto ElfesSylvains => GetRace(IdElfesSylvains);
    public RaceDto HautsElfes => GetRace(IdHautsElfes);
    public RaceDto Humains => GetRace(IdHumains);
    public RaceDto HumainsImperiaux => GetRace(IdHumainsImperiaux);
    //public RaceDto HumainsBretonniens => GetRace(IdHumainsBretonniens);
    //public RaceDto HumainsGospodars => GetRace(IdHumainsGospodars);
    //public RaceDto HumainsUngols => GetRace(IdHumainsUngols);
    //public RaceDto HumainsMutants => GetRace(IdHumainsMutants);
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

    public static int GetIdRegleDesTraitsCulturels(string culture) => culture switch
    {
        "Empire" => 19,
        "Averland" => 60,
        "Hochland" => 61,
        "Middenland" => 62,
        "Nordland" => 63,
        "Ostermark" => 64,
        "Ostland" => 65,
        "Reikland" => 66,
        "Stirland" => 67,
        "Talabecland" => 68,
        "Wissenland" => 69,

        "Sylvanie" => 85,
        "Strigany" => 86,
        "Mutant" => 21,

        "Bretonnie" => 70,
        "L'Anguille" => 71,
        "Aquitanie" => 72,
        "Artenois" => 73,
        "Bastogne" => 74,
        "Bordeleaux" => 75,
        "Brionne" => 76,
        "Couronne" => 77,
        "Gasconnie" => 78,
        "Gisoreux" => 79,
        "Lyonesse" => 80,
        "Monfort" => 81,
        "Mousillon" => 82,
        "Parravon" => 83,
        "Quenelles" => 84,
        _ => -1
    };
}