namespace CharHammer.Services;

public class CampagnesService(IReadOnlyList<CampagneDto> data)
{
    public IReadOnlyList<CampagneDto> AllCampagnes => data;

    public CampagneDto? GetCampagne(int campagneId) => data.FirstOrDefault(c => c.Id == campagneId);

    public SeanceDto? GetSeance(int campagneId, string date) => GetCampagne(campagneId)?.Seances.FirstOrDefault(s => s.Quand == date);

    public IEnumerable<CampagneDto> CampagnesAuxquellesAParticipe(BestioleDto pj)
        => data
            .Where(c => c.Seances.Any(s => s.Pjs.Contains(pj)))
            .OrderBy(c => c.Titre);
}
