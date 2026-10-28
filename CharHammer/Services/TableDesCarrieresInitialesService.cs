namespace CharHammer.Services;

public class TableDesCarrieresInitialesService(IReadOnlyDictionary<int, IReadOnlyList<LigneDeCarriereInitialeDto>> data)
{
    /// <summary>Table des carrières initiales de la race ; vide si la race n'en a pas.</summary>
    public IReadOnlyList<LigneDeCarriereInitialeDto> GetByRace(int idRace)
        => data.TryGetValue(idRace, out var lignes) ? lignes : [];

    /// <summary>
    /// Tire une carrière initiale de la race, pondérée par son facteur, en ignorant les carrières <paramref name="exclues"/>.
    /// </summary>
    /// <returns>La carrière tirée, ou <c>null</c> si la table est vide ou si toutes ses carrières sont exclues.</returns>
    public CarriereDto? GetRandomStartingCareer(int idRace, IReadOnlyCollection<CarriereDto>? exclues = null)
    {
        var lignes = GetByRace(idRace)
            .Where(l => l.Facteur > 0 && (exclues is null || !exclues.Contains(l.Carriere)))
            .ToArray();

        var total = lignes.Sum(l => l.Facteur);
        if (total == 0)
            return null;

        var jet = GenericService.RollDice(total);
        foreach (var ligne in lignes)
        {
            jet -= ligne.Facteur;
            if (jet <= 0)
                return ligne.Carriere;
        }
        return lignes[^1].Carriere;
    }
}
