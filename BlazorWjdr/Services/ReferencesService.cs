namespace BlazorWjdr.Services;

using Models;
using System.Collections.Generic;
using System.Linq;

public class ReferencesService
{
    private readonly Dictionary<int, ReferenceDto> _cacheReference;

    public ReferencesService(Dictionary<int, ReferenceDto> dataReferences)
    {
        _cacheReference = dataReferences;
    }

    public List<ReferenceDto> AllReferences => _cacheReference.Values.ToList();

    private ReferenceDto GetReference(int id)
    {
        return _cacheReference[id];
    }

    public ReferenceDto LivreLesChevaliersDuGraal => GetReference(15);
    public ReferenceDto LivreLeDucheDesDamnes => GetReference(16);
    public ReferenceDto LivreLaReineDesGlaces => GetReference(14);
    public ReferenceDto LivreLesFilsDuRatCornu => GetReference(17);
    public ReferenceDto LivreLeTomeDeLaRedemption => GetReference(13);
}
