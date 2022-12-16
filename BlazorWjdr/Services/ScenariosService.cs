using BlazorWjdr.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Services;

public class ScenariosService
{
    private readonly ScenarioDto[] _scenarios;
    private readonly string[] _auteurs;

    public ScenariosService(IEnumerable<ScenarioDto> scenarios)
    {
        _scenarios = scenarios.OrderBy(s => s.Nom).ToArray();
        _auteurs = _scenarios.SelectMany(s => s.Auteurs).Distinct().OrderBy(a => a).ToArray();
        
    }

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
    public IEnumerable<ScenarioDto> AllScenarios(LieuDto[] lieux, LieuTypeDto[] typesDeLieux)
    {
        var tousLesTypes = new List<LieuTypeDto>(typesDeLieux);
        tousLesTypes.AddRange(lieux.Select(l => l.TypeDeLieu).Distinct());
        return _scenarios.Where(s => s.Lieux.Intersect(lieux).Any() || s.LieuxTypes.Intersect(tousLesTypes).Any());
    }

    public IEnumerable<string> AllAuteurs()
    {
        return _auteurs;
    }
}
