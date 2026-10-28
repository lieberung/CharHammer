namespace CharHammer.Services;

public class TableDesCarrieresInitialesService(IReadOnlyDictionary<int, IReadOnlyList<LigneDeCarriereInitialeDto>> data)
{
    public IReadOnlyList<LigneDeCarriereInitialeDto> GetByRace(int idRace) => data[idRace];

    public CarriereDto GetRandomStartingCareer(int idRace)
    {
        Dictionary<int, CarriereDto> dico = [];
        var key = 0;
        var plage = 0;
        foreach (var carriere in data[idRace].OrderBy(c => c.Carriere.Id))
        {
            plage += carriere.Facteur;
            for (var i = 1; i <= carriere.Facteur; i++)
            {
                key += 1;
                dico[key] = carriere.Carriere;
            }
        }
        
        var dice = GenericService.RollDice(plage);
        return dico[dice];
    }
}