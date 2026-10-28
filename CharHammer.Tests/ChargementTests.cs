using System.IO.Compression;
using System.Text.Json;
using CharHammer.JsonWriter;

namespace CharHammer.Tests;

/// <summary>Le chemin complet des données : sources → validation → data.json.gz → catalogue.</summary>
public class ChargementTests(DonneesReelles donnees)
{
  [Fact]
  public void Les_sources_ne_contiennent_aucune_reference_cassee()
  {
    var erreurs = Validateur.Valider(donnees.Sources);

    Assert.True(erreurs.Count == 0, string.Join(Environment.NewLine, erreurs));
  }

  [Fact]
  public void Le_catalogue_est_construit_a_partir_des_sources()
  {
    Assert.True(donnees.Catalogue.EstCharge);
    Assert.NotEmpty(donnees.Catalogue.Aptitudes.AllAptitudes);
    Assert.NotEmpty(donnees.Catalogue.Carrieres.AllCarrieres);
    Assert.NotEmpty(donnees.Catalogue.Campagnes.AllCampagnes);
  }

  [Fact]
  public void Un_catalogue_non_charge_refuse_de_servir()
  {
    Assert.Throws<InvalidOperationException>(() => new Catalogue().Exiger());
  }

  [Fact]
  public async Task Les_donnees_survivent_a_l_aller_retour_gzip_et_System_Text_Json()
  {
    // Même contrat que JsonWriter (écriture) et DataLoader (lecture) : le contexte généré DataJsonContext.
    using var memoire = new MemoryStream();
    await using (var gzip = new GZipStream(memoire, CompressionLevel.Fastest, leaveOpen: true))
      await JsonSerializer.SerializeAsync(gzip, donnees.Sources, DataJsonContext.Default.DataJson, TestContext.Current.CancellationToken);

    memoire.Position = 0;
    await using var lecture = new GZipStream(memoire, CompressionMode.Decompress);
    var relues = await JsonSerializer.DeserializeAsync(lecture, DataJsonContext.Default.DataJson, TestContext.Current.CancellationToken);

    Assert.NotNull(relues);
    Assert.Equal(donnees.Sources.aptitudes.Length, relues.aptitudes.Length);
    Assert.Equal(donnees.Sources.carrieres.Length, relues.carrieres.Length);
    Assert.Equal(donnees.Sources.creatures.Length, relues.creatures.Length);
    Assert.Equal(donnees.Sources.campagnes.Length, relues.campagnes.Length);

    var catalogue = new Catalogue();
    catalogue.Initialiser(relues);
    Assert.True(catalogue.EstCharge);
  }
}
