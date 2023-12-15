namespace CharHammer.DataSource;

using System.Collections.Generic;

public record ArmeAttributJson(int id, string type, string nom, string description);

public record ArmeJson(
    int id,
    int? parent,
    int[] competences,
    int[]? groupes,
    int[]? attributs,
    string nom,
    string degats,
    string? allonge,
    string? portee,
    string? rechargement,
    string enc,
    string prix,
    string dispo,
    string? description);

public record ArmureJson(
    int id,
    int? parent,
    string nom,
    string type,
    string pa,
    string zones,
    string prix,
    string enc,
    string dispo,
    string description,
    int[]? attributs);

public record RootArme(ICollection<ArmeAttributJson> attributs, ICollection<ArmeJson> armes, ICollection<ArmureJson> armures);