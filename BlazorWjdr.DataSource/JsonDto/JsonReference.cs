namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonReference(int id, string titre, int? publishyear, string code, int version);

public record RootReference(List<JsonReference> items);