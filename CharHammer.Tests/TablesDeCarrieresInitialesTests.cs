namespace CharHammer.Tests;

/// <summary>Tables de tirage des carrières initiales, construites à partir du champ « tirage » des carrières.</summary>
public class TablesDeCarrieresInitialesTests(DonneesReelles donnees)
{
  TableDesCarrieresInitialesService Service => donnees.Catalogue.TablesDesCarrieresInitiales;

  [Theory]
  [InlineData(RacesService.IdHumains)]
  [InlineData(RacesService.IdHumainsBretonniens)]
  [InlineData(RacesService.IdHalflings)]
  [InlineData(RacesService.IdNains)]
  [InlineData(RacesService.IdGnomes)]
  [InlineData(RacesService.IdHautsElfes)]
  [InlineData(RacesService.IdElfesSylvains)]
  public void Chaque_race_jouable_a_une_table(int idRace)
  {
    var table = Service.GetByRace(idRace);

    Assert.NotEmpty(table);
    Assert.All(table, l => Assert.True(l.Facteur > 0, $"{l.Carriere.Nom} : facteur {l.Facteur}"));
    Assert.All(table, l => Assert.Contains(l.Carriere.TirageInitial, t => t.Race.Id == idRace));
  }

  [Fact]
  public void Le_tirage_donne_une_carriere_de_la_table()
  {
    var table = Service.GetByRace(RacesService.IdNains).Select(l => l.Carriere).ToHashSet();

    for (var i = 0; i < 200; i++)
      Assert.Contains(Service.GetRandomStartingCareer(RacesService.IdNains)!, table);
  }

  [Fact]
  public void Le_tirage_respecte_les_exclusions()
  {
    var table = Service.GetByRace(RacesService.IdHautsElfes).Select(l => l.Carriere).ToArray();
    var gardee = table[0];
    var exclues = table.Skip(1).ToArray();

    for (var i = 0; i < 50; i++)
      Assert.Same(gardee, Service.GetRandomStartingCareer(RacesService.IdHautsElfes, exclues));

    Assert.Null(Service.GetRandomStartingCareer(RacesService.IdHautsElfes, table));
  }

  [Fact]
  public void Une_race_sans_table_ne_tire_rien()
  {
    Assert.Empty(Service.GetByRace(-1));
    Assert.Null(Service.GetRandomStartingCareer(-1));
  }
}
