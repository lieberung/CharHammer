using CharHammer.DataSource;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Compression;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace CharHammer.JsonWriter;

/// <summary>Fusionne les fichiers json/*.json saisis à la main en un unique data.json.gz chargé par l'application.</summary>
public class DataService(string dossierSources, string fichierCible)
{
  public void MakeTheFileToRuleThemAll()
  {
    var data = ChargerLesSources(dossierSources);

    // Les références cassées font échouer l'outil (code de sortie 1, donc la CI) avant toute écriture.
    var erreurs = Validateur.Valider(data);
    if (erreurs.Count > 0)
    {
      foreach (var erreur in erreurs)
        Console.Error.WriteLine($"  - {erreur}");
      throw new InvalidDataException($"{erreurs.Count} référence(s) invalide(s) dans les sources : data.json.gz n'a pas été écrit.");
    }
    Console.WriteLine("References checked: OK.");

    Console.Write($"Writing json data to {fichierCible}... ");
    var chrono = Stopwatch.StartNew();
    WriteFile(data);
    Console.WriteLine($"{chrono.ElapsedMilliseconds} ms.");
  }

  /// <summary>Lit et fusionne les fichiers sources du dossier (utilisé aussi par les tests de CharHammer.Tests).</summary>
  public static DataJson ChargerLesSources(string dossierSources)
  {
    Console.Write($"Loading json data from {dossierSources}... ");
    var chrono = Stopwatch.StartNew();

    var aptitudes = Load<RootAptitude>(dossierSources, "aptitude.json");
    var armes = Load<RootArme>(dossierSources, "arme.json");
    var campagnes = Load<RootCampagne>(dossierSources, "campagne.json");
    var creatures = Load<RootCreature>(dossierSources, "creature.json");
    var carrieres = Load<RootCarriere>(dossierSources, "carriere.json");
    var chronologie = Load<RootChrono>(dossierSources, "chrono.json");
    var dieux = Load<RootDieu>(dossierSources, "dieu.json");
    var equipements = Load<RootEquipement>(dossierSources, "equipement.json");
    var lieux = Load<RootLieu>(dossierSources, "lieu.json");
    var races = Load<RootRace>(dossierSources, "race.json");
    var references = Load<RootReference>(dossierSources, "reference.json");
    var regles = Load<RootRegle>(dossierSources, "regle.json");
    var sortileges = Load<RootSortilege>(dossierSources, "sortilege.json");
    var tables = Load<RootTable>(dossierSources, "table.json");
    var scenarios = Load<RootScenario>(dossierSources, "scenarios.json");

    Console.WriteLine($"{chrono.ElapsedMilliseconds} ms.");

    return new DataJson(
        aptitudes.aptitudes,
        armes.attributs,
        armes.armes,
        armes.armures,
        campagnes.users,
        campagnes.campagnes,
        campagnes.teams,
        carrieres.carrieres,
        chronologie.domaines,
        chronologie.chrono,
        creatures.creatures,
        dieux.dieux,
        equipements.equipements,
        lieux.lieuxtypes,
        lieux.lieux,
        races.races,
        references.references,
        regles.regles,
        scenarios.scenarios,
        sortileges.sortileges,
        tables.tables
    );
  }

  /// <summary>Lit un fichier source avec Newtonsoft, plus tolérant que System.Text.Json pour des fichiers saisis à la main.</summary>
  static T Load<T>(string dossierSources, string nomDeFichier)
  {
    var chemin = Path.Combine(dossierSources, nomDeFichier);
    return JsonConvert.DeserializeObject<T>(File.ReadAllText(chemin))
           ?? throw new InvalidDataException($"{chemin} est vide ou invalide.");
  }

  void WriteFile(DataJson data)
  {
    // Écriture avec System.Text.Json et le même contexte généré que celui utilisé par le client pour la lecture :
    // le contrat est garanti identique des deux côtés.
    var options = new System.Text.Json.JsonSerializerOptions
    {
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // garde les accents tels quels (fichier plus compact)
    };

    Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(fichierCible))!);
    using var fichier = File.Create(fichierCible);
    using var gzip = new GZipStream(fichier, CompressionLevel.SmallestSize);
    System.Text.Json.JsonSerializer.Serialize(gzip, data, new DataJsonContext(options).DataJson);
  }
}
