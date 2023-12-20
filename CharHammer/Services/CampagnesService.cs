namespace CharHammer.Services;

public class CampagnesService(IEnumerable<CampagneDto> data)
{
    public IEnumerable<CampagneDto> AllCampagnes => data;

    public SeanceDto GetSeance(int campagneId, string date) => data.First(c => c.Id == campagneId).Seances.First(s => s.Quand == date);

    public IEnumerable<CampagneDto> CampagnesAuxquellesAParticipe(BestioleDto pj)
        => data
            .Where(c => c.Seances.Any(s => s.Pjs.Contains(pj)))
            .OrderBy(c => c.Titre);
}
