namespace CharHammer.DataSource;

using System.Collections.Generic;

public record ReferenceJson(int id, string titre, int? publishyear, int version);

public record RootReference(IEnumerable<ReferenceJson> items);