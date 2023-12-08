namespace BlazorWjdr.DataSource.JsonDto;

using System.Collections.Generic;

public record JsonLieuType(int id, int? parentid, string libelle);

public record JsonLieu(int id, int type, int? parent, string nom, string? population, string? allegeance, string? industrie, string? description, bool ignorer);

public record RootLieu(List<JsonLieuType> types, List<JsonLieu> items);