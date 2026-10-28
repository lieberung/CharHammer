using CharHammer.JsonWriter;
using CharHammer.Tests;

[assembly: AssemblyFixture(typeof(DonneesReelles))]

namespace CharHammer.Tests;

/// <summary>
/// Les vraies données, chargées une seule fois pour toute la session de tests : les sources json/*.json lues comme
/// le fait JsonWriter, puis le <see cref="Catalogue"/> construit comme au démarrage de l'application.
/// </summary>
public sealed class DonneesReelles
{
  public DataJson Sources { get; }
  public Catalogue Catalogue { get; } = new();

  public DonneesReelles()
  {
    var dossierDesSources = Path.Combine(RacineDeLaSolution(), "CharHammer.JsonWriter", "json");
    Sources = DataService.ChargerLesSources(dossierDesSources);
    Catalogue.Initialiser(Sources);
  }

  static string RacineDeLaSolution()
  {
    for (var dossier = new DirectoryInfo(AppContext.BaseDirectory); dossier is not null; dossier = dossier.Parent)
    {
      if (File.Exists(Path.Combine(dossier.FullName, "CharHammer.slnx")))
        return dossier.FullName;
    }
    throw new DirectoryNotFoundException($"CharHammer.slnx introuvable au-dessus de {AppContext.BaseDirectory}.");
  }
}
