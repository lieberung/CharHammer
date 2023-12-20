namespace CharHammer.DataSource;

public record ReferenceJson(int id, string titre, int? publishyear, int version);

public record RootReference(ReferenceJson[] references);