namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonTable(int id, string titre, string description, string[]? styles_th, string[]? styles_td, string[][] lignes);

public record RootTable(List<JsonTable> items);