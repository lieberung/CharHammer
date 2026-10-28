namespace CharHammer.Tests;

/// <summary>Normalisation des textes pour la recherche et les URL.</summary>
public class NormalisationTests(DonneesReelles donnees)
{
  [Theory]
  [InlineData("Prêtre", "pretre")]
  [InlineData("Œil-de-Bœuf", "oeil de boeuf")]
  [InlineData("Ça n’a pas d’æther", "ca n a pas d aether")]
  [InlineData("Épée bâtarde", "epee batarde")]
  [InlineData("Garçon — d'écurie", "garcon d ecurie")]
  [InlineData("L’Armes à feu", "l arme a feu")]
  public void Nettoyer_pour_la_recherche(string saisie, string attendu)
  {
    Assert.Equal(attendu, GenericService.NettoyerPourRecherche(saisie));
  }

  [Theory]
  [InlineData("Armes d'escrime", "armes-d-escrime")]
  [InlineData("Haut-Elfe", "haut-elfe")]
  [InlineData("Arbalètes", "arbaletes")]
  public void Morceau_d_url(string nom, string attendu)
  {
    Assert.Equal(attendu, GenericService.GetUrlChunk(nom));
  }

  [Fact]
  public void Les_mots_vides_sont_ignores()
  {
    Assert.Equal(new[] { "lame", "ombre", "sables" }, GenericService.MotsClefsDeRecherche("la lame de l ombre des sables"));
  }

  [Theory]
  [InlineData("PRÊTRE")]
  [InlineData("pretre")]
  public void La_recherche_de_carrieres_ignore_casse_et_accents(string saisie)
  {
    var pretre = donnees.Catalogue.Carrieres.GetCarriere(189);

    Assert.Contains(pretre, donnees.Catalogue.Carrieres.Recherche(saisie));
  }
}
