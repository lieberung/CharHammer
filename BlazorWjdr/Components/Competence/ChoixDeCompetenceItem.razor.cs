using BlazorWjdr.DomainModel;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Competence
{
    public partial class ChoixDeCompetenceItem
    {
        [Parameter]
        public CompetenceDto[] Choix { get; set; } = null!;
    }
}
