using System.Collections.Generic;

namespace CharHammer.DataSource;

public record UserJson(int id, string email, string pseudo);

public record ContactDeCampagneJson(
    int pnj,
    int lieu_de_rencontre,
    int lieu_de_residence,
    int[]? employeur,
    string[]? notes,
    string? description);

public record CampagneJson(
    int id,
    string titre,
    int mj,
    int team,
    SeanceJson[]? seances,
    ContactDeCampagneJson[]? contacts);

public record TeamJson(int id, string nom);

public record FactJson(int tri, int[]? pjs, string fact);

public record RencontreJson(string groupe, CombattantJson[]? ennemis, CombattantJson[]? allies);

public record CombattantJson(int id, string? nom);

public record SeanceJson(
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
    FactJson[]? facts,
    RencontreJson[]? rencontres);

public record RootCampagne(ICollection<UserJson> users, ICollection<CampagneJson> campagnes, ICollection<TeamJson> teams);