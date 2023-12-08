namespace BlazorWjdr.DataSource.JsonDto;

public record JsonScenario(
    string nom,
    int note,
    string? lien,
    string? duree,
    string? deja_joue,
    string? resume,
    string[]? auteurs,
    string[]? styles,
    int[]? lieux,
    string? source,
    string? commentaire,
    int[]? lieuxtypes,
    string? difficulte
);

public record RootScenario(JsonScenario[] scenarios);