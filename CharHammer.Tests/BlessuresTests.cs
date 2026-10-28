namespace CharHammer.Tests;

/// <summary>Calcul des Blessures selon le gabarit (LdB p. 341) et le trait Dur à cuir.</summary>
public class BlessuresTests(DonneesReelles donnees)
{
  // BF 3, BE 4, BFM 3
  static readonly ProfilDto Profil = new(Cc: 30, Ct: 30, F: 35, E: 42, I: 30, Ag: 30, Dex: 30, Int: 30, Fm: 31, Soc: 30, M: 4, B: 0);

  [Theory]
  [InlineData(AptitudesService.TraitGabaritMinusculeId, false, 1)]
  [InlineData(AptitudesService.TraitGabaritToutPetitId, false, 4)]
  [InlineData(AptitudesService.TraitGabaritPetitId, false, 11)]
  [InlineData(AptitudesService.TraitGabaritMoyenId, false, 14)]
  [InlineData(AptitudesService.TraitGabaritLargeId, false, 28)]
  [InlineData(AptitudesService.TraitGabaritEnormeId, false, 56)]
  [InlineData(AptitudesService.TraitGabaritMonstrueuxId, false, 112)]
  [InlineData(AptitudesService.TraitGabaritMinusculeId, true, 5)]
  [InlineData(AptitudesService.TraitGabaritMoyenId, true, 18)]
  public void Blessures_selon_le_gabarit(int gabaritId, bool durACuir, int attendu)
  {
    var gabarit = donnees.Catalogue.Aptitudes.GetAptitude(gabaritId);

    Assert.Equal(attendu, BestiolesService.CalculBlessures(gabarit, Profil, durACuir));
  }

  [Fact]
  public void Formule_et_detail_du_calcul_pour_un_gabarit_moyen_dur_a_cuir()
  {
    var moyen = donnees.Catalogue.Aptitudes.GetAptitude(AptitudesService.TraitGabaritMoyenId);

    Assert.Equal("BF + (2 * BE) + BFM + BE (dur à cuir)", BestiolesService.GetBlessuresFormuleDeCalcul(moyen, true));
    Assert.Equal("3 + (2 * 4) + 3 + 4*", BestiolesService.GetBlessuresDetailDuCalcul(moyen, Profil, true));
  }

  [Fact]
  public void Un_trait_qui_n_est_pas_un_gabarit_est_refuse()
  {
    var durACuir = donnees.Catalogue.Aptitudes.GetAptitude(AptitudesService.TraitDurACuirId);

    Assert.ThrowsAny<Exception>(() => BestiolesService.CalculBlessures(durACuir, Profil, false));
  }

  [Fact]
  public void Toute_creature_dotee_d_un_profil_a_un_gabarit_et_des_blessures()
  {
    // Les contacts sans profil (E = 0) n'ont pas de Blessures : ils ne combattent pas.
    var creatures = donnees.Sources.creatures
      .Select(c => donnees.Catalogue.Bestioles.GetBestiole(c.id))
      .Where(b => b.ProfilActuel.E > 0)
      .ToArray();

    Assert.NotEmpty(creatures);
    Assert.All(creatures, b =>
    {
      Assert.NotNull(b.Gabarit);
      Assert.True(b.Blessures > 0, $"{b.Nom} ({b.Id}) : {b.Blessures} Blessures");
    });
  }
}
