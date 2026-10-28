using CharHammer.Services;

namespace CharHammer.Helpers;

public static class AptitudeHelper
{
    /// <summary>
    /// Tire au hasard une aptitude qui n'a pas déjà été obtenue et qui n'est incompatible avec aucune de celles-ci.
    /// </summary>
    /// <returns>L'aptitude tirée, ou <c>null</c> s'il ne reste aucune aptitude possible.</returns>
    public static AptitudeDto? TirerUneAptitudeAleatoire(this IEnumerable<AptitudeDto> aptitudes, IReadOnlyCollection<AptitudeDto> dejaObtenues)
    {
        var candidates = aptitudes
            .Where(a => !dejaObtenues.Contains(a)
                        && !a.Incompatibles.Any(i => dejaObtenues.Contains(i))
                        && !dejaObtenues.Any(d => d.Incompatibles.Contains(a)))
            .ToArray();

        return candidates.Length == 0 ? null : candidates[GenericService.RollIndex(candidates.Length)];
    }
}
