using CharHammer.JsonWriter;

namespace CharHammer.Tests;

/// <summary>Le validateur de JsonWriter détecte bien ce qui ferait planter l'application au démarrage.</summary>
public class ValidateurTests(DonneesReelles donnees)
{
  [Fact]
  public void Un_identifiant_en_double_est_signale()
  {
    var sources = donnees.Sources with { aptitudes = [.. donnees.Sources.aptitudes, donnees.Sources.aptitudes[0]] };

    var erreurs = Validateur.Valider(sources);

    Assert.Contains(erreurs, e => e.Contains($"aptitude {donnees.Sources.aptitudes[0].id} en double"));
  }

  [Fact]
  public void Une_reference_vers_un_identifiant_inexistant_est_signalee()
  {
    var carriere = donnees.Sources.carrieres[0];
    var sources = donnees.Sources with { carrieres = [carriere with { metier = 999_999 }, .. donnees.Sources.carrieres.Skip(1)] };

    var erreurs = Validateur.Valider(sources);

    var erreur = Assert.Single(erreurs);
    Assert.Contains($"carrière {carriere.id}", erreur);
    Assert.Contains("metier → aptitude 999999 inexistant", erreur);
  }
}
