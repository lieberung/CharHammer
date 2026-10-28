using CharHammer.JsonWriter;

// Usage : CharHammer.JsonWriter [dossierDesSources] [fichierCible]
// Sans argument, les chemins sont déduits de l'emplacement de la solution (dossier contenant CharHammer.slnx),
// ce qui fonctionne depuis l'IDE comme depuis la ligne de commande ou la CI.
try
{
  var racine = args.Length >= 2 ? null : TrouverRacineDeLaSolution();
  var dossierSources = args.Length >= 1 ? args[0] : Path.Combine(racine!, "CharHammer.JsonWriter", "json");
  var fichierCible = args.Length >= 2 ? args[1] : Path.Combine(racine!, "CharHammer", "wwwroot", "data", "data.json.gz");

  new DataService(dossierSources, fichierCible).MakeTheFileToRuleThemAll();
  Console.WriteLine("Done!");
  return 0;
}
catch (Exception e)
{
  Console.Error.WriteLine($"Échec : {e.Message}");
  return 1;
}

static string TrouverRacineDeLaSolution()
{
  foreach (var depart in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
  {
    for (var dossier = new DirectoryInfo(depart); dossier is not null; dossier = dossier.Parent)
    {
      if (File.Exists(Path.Combine(dossier.FullName, "CharHammer.slnx")))
        return dossier.FullName;
    }
  }
  throw new DirectoryNotFoundException(
      "CharHammer.slnx introuvable : lancez l'outil depuis la solution, ou passez le dossier des sources et le fichier cible en arguments.");
}
