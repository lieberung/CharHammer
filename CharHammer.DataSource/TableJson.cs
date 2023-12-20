namespace CharHammer.DataSource;

public record TableJson(int id, string titre, string description, string[]? styles_th, string[]? styles_td, string[][] lignes);

public record RootTable(TableJson[] tables);