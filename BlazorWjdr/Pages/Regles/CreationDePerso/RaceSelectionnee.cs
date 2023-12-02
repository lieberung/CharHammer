using BlazorWjdr.Models;
using BlazorWjdr.Services;

namespace BlazorWjdr.Pages.Regles.CreationDePerso;

public record RaceSelectionnee(RaceDto Race)
{
    public bool RaceDesElfesSylvainsSelectionnee => Race.Id == RacesService.IdElfesSylvains;
    public bool RaceDesHautsElfesSelectionnee => Race.Id == RacesService.IdHautsElfes;
    public bool RaceDesGnomesSelectionnee => Race.Id == RacesService.IdGnomes;
    public bool RaceDesHalflingsSelectionnee => Race.Id == RacesService.IdHalflings;
    public bool RaceDesHumainsSelectionnee => Race.Id == RacesService.IdHumains;
    public bool RaceDesNainsSelectionnee => Race.Id == RacesService.IdNains;
}
