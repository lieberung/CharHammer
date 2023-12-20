namespace CharHammer.DataSource;

public record LieuTypeJson(int id, int? parentid, string libelle);

public record LieuJson(int id, int type, int? parent, string nom, string? population, string? allegeance, string? industrie, string? description, bool ignorer);

public record RootLieu(LieuTypeJson[] lieuxtypes, LieuJson[] lieux);