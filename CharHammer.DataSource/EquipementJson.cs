using System.Collections.Generic;

namespace CharHammer.DataSource;

public record IngredientsJson(string prix, string localisation, string difficulte);

public record CreationJson(string difficulte, string temps);

public record PotionJson(string reaction, string instabilite, IngredientsJson ingredients, CreationJson creation);

public record EquipementJson(
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
    PotionJson? potion);

public record RootEquipement(IEnumerable<EquipementJson> items);