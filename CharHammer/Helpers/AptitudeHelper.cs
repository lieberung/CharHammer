using CharHammer.Services;

namespace CharHammer.Helpers;

public static class AptitudeHelper
{
    public static AptitudeDto TirerUneAptitudeAleatoire(this IEnumerable<AptitudeDto> aptitudes, AptitudeDto[] dejaObtenues)
    {
        var exclusions = dejaObtenues.Union(dejaObtenues.SelectMany(a => a.Incompatibles)).ToArray();
        var liste = aptitudes.Except(exclusions).ToArray();

        AptitudeDto? ta = null;
        while (ta is null || ta.Incompatibles.Intersect(dejaObtenues).Any())
        {
            var i = GenericService.RollIndex(liste.Length);
            ta = liste[i];
        }
        return ta;
    }
}
