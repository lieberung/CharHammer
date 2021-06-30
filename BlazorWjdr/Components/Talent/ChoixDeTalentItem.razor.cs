using BlazorWjdr.DomainModel;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Talent
{
    public partial class ChoixDeTalentItem
    {
        [Parameter]
        public TalentDto[] Choix { get; set; } = null!;
    }
}
