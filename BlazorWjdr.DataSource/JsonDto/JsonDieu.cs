namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonOrdre(int id, string? nom, int[]? aptitudes, string? description, bool mineur, string? ambiance);

public record JsonAptitudesAssociees(int[]? initie, int[]? pretre_sans_ordre);

public record JsonRegleAssociee(string? titre, string? description);

public record JsonSecte(string? nom, string? description);

public record JsonTemple(string? nom, string? description);

public record JsonPersonnalite(string? nom, string? description);

public record JsonDomaine(int id, string nom, string comment);

public record JsonDieu(
    int id,
    int? patron,
    string nom,
    string? domaines,
    string? fideles,
    string? histoire,
    string? comment,
    string? symboles,
    int? siege,
    JsonAptitudesAssociees? aptitudes,
    string? chef,
    string? fetes,
    string? livres,
    string? intro,
    string? penitences,
    string? culte,
    JsonRegleAssociee[]? regles,
    string? dogme,
    string? initiation,
    string? pretrise,
    string[]? commandements,
    string? cultistes,
    string? structure,
    JsonSecte[]? sectes,
    string[]? ambiance,
    JsonTemple[]? temples,
    JsonPersonnalite[]? personnalites,
    JsonOrdre[]? ordres);

public record RootDieu(IEnumerable<JsonDieu> items);