using System.Reflection;

namespace CharHammer.Tests;

/// <summary>
/// Les identifiants écrits en dur dans le code (constantes et propriétés comme <c>TraitGuerrierNe</c>,
/// <c>CarriereInitie</c>…) doivent tous exister dans les données : une faute de frappe ferait planter une page.
/// </summary>
public class IdentifiantsNommesTests(DonneesReelles donnees)
{
  public static TheoryData<string> Services()
  {
    var services = new TheoryData<string>();
    foreach (var propriete in typeof(Catalogue).GetProperties().Where(p => p.PropertyType.Namespace == typeof(AptitudesService).Namespace))
      services.Add(propriete.Name);
    return services;
  }

  [Theory]
  [MemberData(nameof(Services))]
  public void Toutes_les_proprietes_publiques_du_service_se_lisent_sans_erreur(string nomDuService)
  {
    var service = typeof(Catalogue).GetProperty(nomDuService)!.GetValue(donnees.Catalogue)!;
    var echecs = new List<string>();

    foreach (var propriete in service.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
    {
      if (propriete.GetIndexParameters().Length > 0)
        continue;
      try
      {
        propriete.GetValue(propriete.GetMethod!.IsStatic ? null : service);
      }
      catch (TargetInvocationException e)
      {
        echecs.Add($"{nomDuService}.{propriete.Name} : {e.InnerException?.Message}");
      }
    }

    Assert.True(echecs.Count == 0, string.Join(Environment.NewLine, echecs));
  }

  [Fact]
  public void Les_constantes_d_aptitudes_existent()
  {
    var ids = donnees.Catalogue.Aptitudes.AllAptitudes.Select(a => a.Id).ToHashSet();
    var constantes = ConstantesEntieres(typeof(AptitudesService)).Append(new Constante("TalentReflexesDeCombatId", AptitudesService.TalentReflexesDeCombatId));

    Assert.All(constantes, c => Assert.True(ids.Contains(c.Valeur), $"AptitudesService.{c.Nom} = {c.Valeur} n'existe pas dans aptitude.json"));
  }

  [Fact]
  public void Les_constantes_de_races_existent()
  {
    var ids = donnees.Catalogue.Races.AllRaces.Select(r => r.Id).ToHashSet();

    Assert.All(ConstantesEntieres(typeof(RacesService)), c => Assert.True(ids.Contains(c.Valeur), $"RacesService.{c.Nom} = {c.Valeur} n'existe pas dans race.json"));
  }

  [Fact]
  public void Les_carrieres_du_chaos_existent()
  {
    var ids = donnees.Catalogue.Carrieres.AllCarrieres.Select(c => c.Id).ToHashSet();

    Assert.All(CarrieresService.CarrieresChaos, id => Assert.True(ids.Contains(id), $"Carrière {id} absente de carriere.json"));
  }

  [Theory]
  [InlineData("Empire"), InlineData("Averland"), InlineData("Hochland"), InlineData("Middenland"), InlineData("Nordland")]
  [InlineData("Ostermark"), InlineData("Ostland"), InlineData("Reikland"), InlineData("Stirland"), InlineData("Talabecland")]
  [InlineData("Wissenland"), InlineData("Sylvanie"), InlineData("Strigany"), InlineData("Mutant"), InlineData("Bretonnie")]
  [InlineData("L'Anguille"), InlineData("Aquitanie"), InlineData("Artenois"), InlineData("Bastogne"), InlineData("Bordeleaux")]
  [InlineData("Brionne"), InlineData("Couronne"), InlineData("Gasconnie"), InlineData("Gisoreux"), InlineData("Lyonesse")]
  [InlineData("Monfort"), InlineData("Mousillon"), InlineData("Parravon"), InlineData("Quenelles")]
  [InlineData("Kislev"), InlineData("Gospodars"), InlineData("Ungols"), InlineData("Norsca")]
  public void Chaque_culture_a_sa_regle_de_traits_culturels(string culture)
  {
    var id = RacesService.GetIdRegleDesTraitsCulturels(culture);

    Assert.NotEqual(-1, id);
    Assert.NotNull(donnees.Catalogue.Regles.GetRegle(id));
  }

  [Fact]
  public void Une_culture_inconnue_n_a_pas_de_regle()
  {
    Assert.Equal(-1, RacesService.GetIdRegleDesTraitsCulturels("Lustrie"));
  }

  sealed record Constante(string Nom, int Valeur);

  static IEnumerable<Constante> ConstantesEntieres(Type type)
    => type.GetFields(BindingFlags.Public | BindingFlags.Static)
      .Where(f => f is { IsLiteral: true } && f.FieldType == typeof(int))
      .Select(f => new Constante(f.Name, (int)f.GetRawConstantValue()!));
}
