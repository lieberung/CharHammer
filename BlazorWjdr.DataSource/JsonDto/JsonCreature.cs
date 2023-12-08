namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonCreature(
    int id,
    JsonProfil profil_actuel,
    int? user,
    int race,
    string nom,
    string? nom_court,
    int? sexe,
    int[]? origines,
    string[]? membrede,
    int? poids,
    int? taille,
    int? age,
    int[]? aptitudes,
    int[]? aptitudes_facultatives,
    string? description,
    string? psycho,
    string? histoire,
    string[]? ambitions,
    string? notes,
    string? date_creation,
    int? xp_actuel,
    int? xp_total,
    JsonProfil? profil_initial,
    int[]? cheminement,
    int? carriere_du_pere,
    int? carriere_de_la_mere,
    int? fk_signeastralid,
    string? freres_et_soeurs,
    int? main_directrice,
    bool masquer,
    bool mort,
    string? cheveux,
    string? yeux,
    int[]? armes,
    int[]? armures,
    int[]? equipement,
    int[]? sorts);

public record RootCreature(List<JsonCreature> items);