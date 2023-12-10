namespace CharHammer.Services;

using Models;
using System.Collections.Generic;
using System.Linq;

public class DieuxService(IReadOnlyDictionary<int, DieuDto> dataDieux)
{
    //public IEnumerable<DieuDto> AllDieux { get; } dataDieux.Values.OrderBy(d => d.Nom).ToArray();

    public DieuDto GetDieu(int id) => dataDieux[id];

    public string NomDuCulte(int idCulte)
    {
        var dieu = dataDieux.Values.First(d => d.Ordres.Any(o => o.Id == idCulte));
        var culte = dieu.Ordres.First(o => o.Id == idCulte);

        return culte.Nom.Contains(dieu.Nom) ? culte.Nom : $"{culte.Nom} ({dieu.Nom})";
    }
}