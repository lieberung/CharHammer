namespace BlazorWjdr.DataSource.JsonDto;

public record JsonUser(int id, string email, string pseudo);

public record JsonContactDeCampagne(
    int pnj,
    int lieu_de_rencontre,
    int lieu_de_residence,
    int[]? employeur,
    string[]? notes,
    string? description);

public record JsonCampagne(
    int id,
    string titre,
    int mj,
    int team,
    JsonSeance[]? seances,
    JsonContactDeCampagne[]? contacts);

public record JsonTeam(int id, string nom);

public record JsonFact(int tri, int[]? pjs, string fact);

public record JsonRencontre(string groupe, JsonCombattant[]? ennemis, JsonCombattant[]? allies);

public record JsonCombattant(int id, string? nom);

public record JsonSeance(
    string quand,
    bool secret,
    int acte,
    string? debut,
    int duree,
    string titre,
    string? scenario,
    int xp,
    string? xp_comment,
    string? resume,
    int[]? lieux,
    int[]? pjs,
    JsonFact[]? facts,
    JsonRencontre[]? rencontres);

public record RootCampagne(JsonUser[] users, JsonCampagne[] campagnes, JsonTeam[] teams);