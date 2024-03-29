﻿using CharHammer.DataSource;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;

namespace CharHammer.JsonWriter;

public class DataService
{
  const string JsonDataSourcePath = @"..\..\..\json";
  const string JsonDataTargetPath = @"..\..\..\..\CharHammer\wwwroot\data";
  const string JsonDataTargetFile = @"\data.json.gz";

  public void MakeTheFileToRuleThemAll()
  {
    var data = BuildDataInstance();
    var dataToJson = JsonConvert.SerializeObject(data
        , new JsonSerializerSettings { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore });
    Console.WriteLine($"Writing json data to {JsonDataTargetPath}{JsonDataTargetFile}...");
    WriteFile(dataToJson);
    Console.WriteLine("Done!");
    Console.ReadKey();
  }

  DataJson BuildDataInstance()
  {
    Console.Write("Loading json data... ");
    var startTime = DateTime.Now;
    Aptitudes = GetAptitudes();
    Armes = GetArmes();
    Campagne = GetCampagnes();
    Creatures = GetCreatures();
    Carrieres = GetCarrieres();
    Chrono = GetChrono();
    Dieux = GetDieux();
    Equipements = GetEquipement();
    Lieux = GetLieux();
    Races = GetRaces();
    References = GetReferences();
    Regles = GetRegles();
    Sortileges = GetSortileges();
    Tables = GetTables();
    Scenarios = GetScenarios();
    Console.WriteLine($"{DateTime.Now.Subtract(startTime).TotalSeconds}sec.");

    return new DataJson(
        Aptitudes.aptitudes,
        Armes.attributs,
        Armes.armes,
        Armes.armures,
        Campagne.users,
        Campagne.campagnes,
        Campagne.teams,
        Carrieres.carrieres,
        Chrono.domaines,
        Chrono.chrono,
        Creatures.creatures,
        Dieux.dieux,
        Equipements.equipements,
        Lieux.lieuxtypes,
        Lieux.lieux,
        Races.races,
        References.references,
        Regles.regles,
        Scenarios.scenarios,
        Sortileges.sortileges,
        Tables.tables
    );
  }

  RootAptitude? Aptitudes { get; set; }
  RootCampagne? Campagne { get; set; }
  RootScenario? Scenarios { get; set; }
  RootCreature? Creatures { get; set; }
  RootCarriere? Carrieres { get; set; }
  RootChrono? Chrono { get; set; }
  RootDieu? Dieux { get; set; }
  RootLieu? Lieux { get; set; }
  RootArme? Armes { get; set; }
  RootRace? Races { get; set; }
  RootSortilege? Sortileges { get; set; }
  RootReference? References { get; set; }
  RootTable? Tables { get; set; }
  RootRegle? Regles { get; set; }
  RootEquipement? Equipements { get; set; }

  private static T LoadRootFromJson<T>(string path)
  {
    using StreamReader r = new(path);
    var json = r.ReadToEnd();
    return JsonConvert.DeserializeObject<T>(json)!;
  }

  private static RootAptitude GetAptitudes() => LoadRootFromJson<RootAptitude>($"{JsonDataSourcePath}\\aptitude.json");
  private static RootEquipement GetEquipement() => LoadRootFromJson<RootEquipement>($"{JsonDataSourcePath}\\equipement.json");
  private static RootCampagne GetCampagnes() => LoadRootFromJson<RootCampagne>($"{JsonDataSourcePath}\\campagne.json");
  private static RootCreature GetCreatures() => LoadRootFromJson<RootCreature>($"{JsonDataSourcePath}\\creature.json");
  private static RootCarriere GetCarrieres() => LoadRootFromJson<RootCarriere>($"{JsonDataSourcePath}\\carriere.json");
  private static RootChrono GetChrono() => LoadRootFromJson<RootChrono>($"{JsonDataSourcePath}\\chrono.json");
  private static RootDieu GetDieux() => LoadRootFromJson<RootDieu>($"{JsonDataSourcePath}\\dieu.json");
  private static RootLieu GetLieux() => LoadRootFromJson<RootLieu>($"{JsonDataSourcePath}\\lieu.json");
  private static RootArme GetArmes() => LoadRootFromJson<RootArme>($"{JsonDataSourcePath}\\arme.json");
  private static RootRace GetRaces() => LoadRootFromJson<RootRace>($"{JsonDataSourcePath}\\race.json");
  private static RootSortilege GetSortileges() => LoadRootFromJson<RootSortilege>($"{JsonDataSourcePath}\\sortilege.json");
  private static RootReference GetReferences() => LoadRootFromJson<RootReference>($"{JsonDataSourcePath}\\reference.json");
  private static RootTable GetTables() => LoadRootFromJson<RootTable>($"{JsonDataSourcePath}\\table.json");
  private static RootRegle GetRegles() => LoadRootFromJson<RootRegle>($"{JsonDataSourcePath}\\regle.json");
  private static RootScenario GetScenarios() => LoadRootFromJson<RootScenario>($"{JsonDataSourcePath}\\scenarios.json");

  //private static DataJson GetData() => LoadRootFromJson<DataJson>(JsonDataTargetPath);

  static void WriteFile(string dataInput)
  {
    using var fs = File.Create(JsonDataTargetPath + JsonDataTargetFile);
    var info = CompressToGzip(dataInput);
    fs.Write(info, 0, info.Length);
  }

  static byte[] CompressToGzip(string inputStr)
  {
    var inputBytes = Encoding.UTF8.GetBytes(inputStr);

    using var outputStream = new MemoryStream();
    using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
      gZipStream.Write(inputBytes, 0, inputBytes.Length);

    return outputStream.ToArray();
  }
}
