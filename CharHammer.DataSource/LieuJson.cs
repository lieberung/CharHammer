namespace CharHammer.DataSource;

using System.Collections.Generic;

public record LieuTypeJson(int id, int? parentid, string libelle);

public record LieuJson(int id, int type, int? parent, string nom, string? population, string? allegeance, string? industrie, string? description, bool ignorer);

public record RootLieu(ICollection<LieuTypeJson> lieuxtypes, ICollection<LieuJson> lieux);