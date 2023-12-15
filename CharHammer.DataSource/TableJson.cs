namespace CharHammer.DataSource;

using System.Collections.Generic;

public record TableJson(int id, string titre, string description, string[]? styles_th, string[]? styles_td, string[][] lignes);

public record RootTable(ICollection<TableJson> items);