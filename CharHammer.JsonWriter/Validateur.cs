using CharHammer.DataSource;

namespace CharHammer.JsonWriter;

/// <summary>
/// Vérifie, avant l'écriture de data.json.gz, que chaque identifiant cité dans les fichiers json/*.json existe
/// et qu'aucun identifiant n'est en double. Ce sont exactement les références que l'Initializer de l'application
/// résout au démarrage : une erreur ici serait sinon une page « Impossible de charger » dans le navigateur.
/// </summary>
internal sealed class Validateur
{
  private readonly List<string> _erreurs = [];

  public static IReadOnlyList<string> Valider(DataJson data)
  {
    var validateur = new Validateur();
    validateur.ValiderTout(data);
    return validateur._erreurs;
  }

  private void ValiderTout(DataJson d)
  {
    // Index des identifiants (et détection des doublons)
    var aptitudes = Index("aptitude.json", "aptitude", d.aptitudes, a => a.id);
    var attributs = Index("arme.json", "attribut", d.attributs, a => a.id);
    var armes = Index("arme.json", "arme", d.armes, a => a.id);
    var armures = Index("arme.json", "armure", d.armures, a => a.id);
    var utilisateurs = Index("campagne.json", "utilisateur", d.users, u => u.id);
    var equipes = Index("campagne.json", "équipe", d.teams, t => t.id);
    Index("campagne.json", "campagne", d.campagnes, c => c.id);
    var carrieres = Index("carriere.json", "carrière", d.carrieres, c => c.id);
    var domaines = Index("chrono.json", "domaine", d.domaines, x => x.id);
    var creatures = Index("creature.json", "créature", d.creatures, c => c.id);
    var dieux = Index("dieu.json", "dieu", d.dieux, x => x.id);
    var equipements = Index("equipement.json", "équipement", d.equipements, e => e.id);
    var typesDeLieu = Index("lieu.json", "type de lieu", d.lieuxtypes, t => t.id);
    var lieux = Index("lieu.json", "lieu", d.lieux, l => l.id);
    var races = Index("race.json", "race", d.races, r => r.id);
    var references = Index("reference.json", "référence", d.references, r => r.id);
    var regles = Index("regle.json", "règle", d.regles, r => r.id);
    var sortileges = Index("sortilege.json", "sortilège", d.sortileges, s => s.id);
    var tables = Index("table.json", "table", d.tables, t => t.id);
    var scenarios = IndexDesNoms("scenarios.json", "scénario", d.scenarios.Select(s => s.nom));

    foreach (var a in d.aptitudes)
    {
      var ou = $"aptitude.json : aptitude {a.id} « {a.nom} »";
      Ref(ou, "parent", a.parent, aptitudes, "aptitude");
      Ref(ou, "aptitudes", a.aptitudes, aptitudes, "aptitude");
      Ref(ou, "incompatibles", a.incompatibles, aptitudes, "aptitude");
    }

    foreach (var a in d.armes)
    {
      var ou = $"arme.json : arme {a.id} « {a.nom} »";
      Ref(ou, "competences", a.competences, aptitudes, "aptitude");
      Ref(ou, "groupes", a.groupes, attributs, "attribut");
      Ref(ou, "attributs", a.attributs, attributs, "attribut");
    }

    foreach (var a in d.armures)
      Ref($"arme.json : armure {a.id} « {a.nom} »", "attributs", a.attributs, attributs, "attribut");

    foreach (var c in d.campagnes)
    {
      var ou = $"campagne.json : campagne {c.id} « {c.titre} »";
      Ref(ou, "mj", c.mj, utilisateurs, "utilisateur");
      Ref(ou, "team", c.team, equipes, "équipe");

      foreach (var s in c.seances ?? [])
      {
        var seance = $"{ou}, séance du {s.quand}";
        Ref(seance, "lieux", s.lieux, lieux, "lieu");
        Ref(seance, "pjs", s.pjs, creatures, "créature");
        foreach (var f in s.facts ?? [])
          Ref(seance, "facts.pjs", f.pjs, creatures, "créature");
        foreach (var r in s.rencontres ?? [])
        {
          Ref($"{seance}, rencontre « {r.groupe} »", "allies", r.allies?.Select(x => x.id), creatures, "créature");
          Ref($"{seance}, rencontre « {r.groupe} »", "ennemis", r.ennemis?.Select(x => x.id), creatures, "créature");
        }
        if (s.scenario is not null && !scenarios.Contains(s.scenario))
          _erreurs.Add($"{seance}, scenario → scénario « {s.scenario} » inexistant");
      }

      foreach (var k in c.contacts ?? [])
      {
        var contact = $"{ou}, contact {k.pnj}";
        Ref(contact, "pnj", k.pnj, creatures, "créature");
        Ref(contact, "lieu_de_rencontre", k.lieu_de_rencontre, lieux, "lieu");
        if (k.lieu_de_residence != 0) // 0 : pas de lieu de résidence
          Ref(contact, "lieu_de_residence", k.lieu_de_residence, lieux, "lieu");
        Ref(contact, "employeur", k.employeur, carrieres, "carrière");
      }
    }

    foreach (var c in d.carrieres)
    {
      var ou = $"carriere.json : carrière {c.id} « {c.nom} »";
      Ref(ou, "parent", c.parent, carrieres, "carrière");
      Ref(ou, "debouch", c.debouch, carrieres, "carrière");
      Ref(ou, "avancements", c.avancements, carrieres, "carrière");
      Ref(ou, "tirage", c.tirage?.Select(t => t.r), races, "race");
      Ref(ou, "metier", c.metier, aptitudes, "aptitude");
      Ref(ou, "aptitudes", c.aptitudes, aptitudes, "aptitude");
      foreach (var choix in c.aptitudes_choix ?? [])
        Ref(ou, "aptitudes_choix", choix, aptitudes, "aptitude");
      Ref(ou, "source", c.source?.id, references, "référence");
    }

    foreach (var c in d.chrono)
    {
      var ou = $"chrono.json : événement {c.debut} « {c.titre ?? c.resume} »";
      Ref(ou, "sources", c.sources, references, "référence");
      Ref(ou, "domaines", c.domaines, domaines, "domaine");
    }

    foreach (var c in d.creatures)
    {
      var ou = $"creature.json : créature {c.id} « {c.nom} »";
      Ref(ou, "race", c.race, races, "race");
      Ref(ou, "user", c.user, utilisateurs, "utilisateur");
      Ref(ou, "aptitudes", c.aptitudes, aptitudes, "aptitude");
      Ref(ou, "aptitudes_facultatives", c.aptitudes_facultatives, aptitudes, "aptitude");
      Ref(ou, "origines", c.origines, lieux, "lieu");
      Ref(ou, "armes", c.armes, armes, "arme");
      Ref(ou, "armures", c.armures, armures, "armure");
      Ref(ou, "equipement", c.equipement, equipements, "équipement");
      Ref(ou, "sorts", c.sorts, sortileges, "sortilège");
      Ref(ou, "cheminement", c.cheminement, carrieres, "carrière");
      Ref(ou, "carriere_du_pere", c.carriere_du_pere, carrieres, "carrière");
      Ref(ou, "carriere_de_la_mere", c.carriere_de_la_mere, carrieres, "carrière");
    }

    foreach (var c in d.dieux)
    {
      var ou = $"dieu.json : dieu {c.id} « {c.nom} »";
      Ref(ou, "patron", c.patron, dieux, "dieu");
      Ref(ou, "siege", c.siege, lieux, "lieu");
      Ref(ou, "aptitudes.initie", c.aptitudes?.initie, aptitudes, "aptitude");
      Ref(ou, "aptitudes.pretre_sans_ordre", c.aptitudes?.pretre_sans_ordre, aptitudes, "aptitude");
      foreach (var o in c.ordres ?? [])
        Ref($"{ou}, ordre {o.id} « {o.nom} »", "aptitudes", o.aptitudes, aptitudes, "aptitude");
    }

    foreach (var e in d.equipements)
    {
      var ou = $"equipement.json : équipement {e.id} « {e.nom} »";
      Ref(ou, "parent", e.parent, equipements, "équipement");
      Ref(ou, "lieux", e.lieux, lieux, "lieu");
      Ref(ou, "lieuxtypes", e.lieuxtypes, typesDeLieu, "type de lieu");
    }

    foreach (var t in d.lieuxtypes)
      Ref($"lieu.json : type de lieu {t.id} « {t.libelle} »", "parentid", t.parentid, typesDeLieu, "type de lieu");

    foreach (var l in d.lieux)
    {
      var ou = $"lieu.json : lieu {l.id} « {l.nom} »";
      Ref(ou, "type", l.type, typesDeLieu, "type de lieu");
      Ref(ou, "parent", l.parent, lieux, "lieu");
    }

    foreach (var r in d.races)
    {
      var ou = $"race.json : race {r.id} « {r.nom_masculin} »";
      Ref(ou, "parent", r.parent, races, "race");
      Ref(ou, "lieux", r.lieux, lieux, "lieu");
      Ref(ou, "aptitudes", r.aptitudes, aptitudes, "aptitude");
      Ref(ou, "opinions", r.opinions?.Where(o => o.race != 0).Select(o => o.race), races, "race"); // 0 : opinion générale
    }

    foreach (var r in d.regles)
    {
      var ou = $"regle.json : règle {r.id} « {r.titre} »";
      Ref(ou, "regles", r.regles, regles, "règle");
      Ref(ou, "carrieres", r.carrieres, carrieres, "carrière");
      Ref(ou, "aptitudes", r.aptitudes, aptitudes, "aptitude");
      foreach (var choix in r.aptitudes_choix ?? [])
        Ref(ou, "aptitudes_choix", choix, aptitudes, "aptitude");
      Ref(ou, "bestioles", r.bestioles, creatures, "créature");
      Ref(ou, "tables", r.tables, tables, "table");
      Ref(ou, "lieux", r.lieux, lieux, "lieu");
    }

    foreach (var s in d.scenarios)
    {
      var ou = $"scenarios.json : scénario « {s.nom} »";
      Ref(ou, "lieux", s.lieux, lieux, "lieu");
      Ref(ou, "lieuxtypes", s.lieuxtypes, typesDeLieu, "type de lieu");
    }

    foreach (var s in d.sortileges)
      Ref($"sortilege.json : sortilège {s.id} « {s.nom} »", "aptitudes", s.aptitudes, aptitudes, "aptitude");
  }

  private HashSet<int> Index<T>(string fichier, string type, IEnumerable<T> elements, Func<T, int> id)
  {
    var ids = new HashSet<int>();
    foreach (var element in elements)
    {
      if (!ids.Add(id(element)))
        _erreurs.Add($"{fichier} : {type} {id(element)} en double");
    }
    return ids;
  }

  // Les séances retrouvent leur scénario par le nom, sans tenir compte de la casse (voir Initializer).
  private HashSet<string> IndexDesNoms(string fichier, string type, IEnumerable<string> noms)
  {
    var index = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
    foreach (var nom in noms)
    {
      if (!index.Add(nom))
        _erreurs.Add($"{fichier} : {type} « {nom} » en double");
    }
    return index;
  }

  private void Ref(string ou, string champ, IEnumerable<int>? ids, HashSet<int> cibles, string cible)
  {
    foreach (var id in ids ?? [])
    {
      if (!cibles.Contains(id))
        _erreurs.Add($"{ou}, {champ} → {cible} {id} inexistant");
    }
  }

  private void Ref(string ou, string champ, int? id, HashSet<int> cibles, string cible)
  {
    if (id is not null)
      Ref(ou, champ, new[] { id.Value }, cibles, cible);
  }
}
