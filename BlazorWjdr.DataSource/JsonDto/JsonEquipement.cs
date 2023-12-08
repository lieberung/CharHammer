using System.Collections.Generic;

namespace BlazorWjdr.DataSource.JsonDto;

public record JsonIngredients(string prix, string localisation, string difficulte);

public record JsonCreation(string difficulte, string temps);

public record JsonPotion(string reaction, string instabilite, JsonIngredients ingredients, JsonCreation creation);

public record JsonEquipement(
    int id,
    int? parent,
    string[]? groupes,
    string nom,
    string prix,
    string? enc,
    string dispo,
    string description,
    string? contenance,
    int? addiction,
    int[]? lieux,
    int[]? lieuxtypes,
    JsonPotion? potion);

public record RootEquipement(List<JsonEquipement> items);