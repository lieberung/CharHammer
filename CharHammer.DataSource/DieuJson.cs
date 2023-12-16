namespace CharHammer.DataSource;

using System.Collections.Generic;

public record OrdreJson(int id, string? nom, int[]? aptitudes, string? description, bool mineur, string? ambiance);

public record AptitudesAssocieesJson(int[]? initie, int[]? pretre_sans_ordre);

public record RegleAssocieeJson(string? titre, string? description);

public record SecteJson(string? nom, string? description);

public record TempleJson(string? nom, string? description);

public record PersonnaliteJson(string? nom, string? description);

public record DieuJson(
    int id,
    int? patron,
    string nom,
    string? domaines,
    string? fideles,
    string? histoire,
    string? comment,
    string? symboles,
    int? siege,
    AptitudesAssocieesJson? aptitudes,
    string? chef,
    string? fetes,
    string? livres,
    string? intro,
    string? penitences,
    string? culte,
    RegleAssocieeJson[]? regles,
    string? dogme,
    string? initiation,
    string? pretrise,
    string[]? commandements,
    string? cultistes,
    string? structure,
    SecteJson[]? sectes,
    string[]? ambiance,
    TempleJson[]? temples,
    PersonnaliteJson[]? personnalites,
    OrdreJson[]? ordres);

public record RootDieu(ICollection<DieuJson> dieux);