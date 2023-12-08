namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonArmeAttribut(int id, string type, string nom, string description);

public record JsonArme(
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

public record JsonArmure(
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

public record RootArme(IEnumerable<JsonArmeAttribut> attributs, IEnumerable<JsonArme> armes, IEnumerable<JsonArmure> armures);