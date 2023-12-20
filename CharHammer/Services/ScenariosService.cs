namespace CharHammer.Services;

public class ScenariosService
{
    public ScenariosService(IEnumerable<ScenarioDto> scenarios)
    {
        _scenarios = scenarios.OrderBy(s => s.Nom).ToArray();
        AllAuteurs = _scenarios.SelectMany(s => s.Auteurs).Distinct().OrderBy(a => a);
    }

    private readonly IEnumerable<ScenarioDto> _scenarios;
    public IEnumerable<string> AllAuteurs { get; }

    private static string Npr(string s) => GenericService.NettoyerPourRecherche(s);

    public IEnumerable<ScenarioDto> AllScenarios(bool pasDeDaubes, string filtre, string auteur)
    {
        filtre = Npr(filtre);
        return _scenarios.Where(s =>
            (s.Note is 0 or > 2 || pasDeDaubes == false) &&
            (filtre == "" || Npr(s.Nom).Contains(filtre) || Npr(s.Source).Contains(filtre)) &&
            (auteur == "" || s.Auteurs.Contains(auteur))
        );
    }
    public IEnumerable<ScenarioDto> AllScenarios(IEnumerable<LieuDto> lieux, IEnumerable<LieuTypeDto> typesDeLieux)
    {
        var tousLesTypes = typesDeLieux.Union(lieux.Select(l => l.TypeDeLieu)).Distinct();
        return _scenarios.Where(s => s.Lieux.Intersect(lieux).Any() || s.LieuxTypes.Intersect(tousLesTypes).Any());
    }

}
