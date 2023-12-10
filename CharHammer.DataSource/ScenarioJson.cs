namespace CharHammer.DataSource;

public record ScenarioJson(
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
    int[]? lieuxtypes,
    string? difficulte
);

public record RootScenario(ScenarioJson[] scenarios);