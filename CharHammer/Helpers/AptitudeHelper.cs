using CharHammer.Models;
using CharHammer.Services;

namespace CharHammer.Helpers;

public static class AptitudeHelper
{
    public static AptitudeDto TirerUneAptitudeAleatoire(this IEnumerable<AptitudeDto> aptitudes, IEnumerable<AptitudeDto> dejaObtenues)
    {
        var exclusions = dejaObtenues.Union(dejaObtenues.SelectMany(a => a.Incompatibles));
        var liste = aptitudes.Except(exclusions);

        AptitudeDto? ta = null;
        while (ta is null || ta.Incompatibles.Intersect(dejaObtenues).Any())
        {
            var i = GenericService.RollIndex(liste.Count());
            ta = liste.ElementAt(i);
        }
        return ta;
    }
}
