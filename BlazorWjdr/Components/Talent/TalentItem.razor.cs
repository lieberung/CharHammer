using BlazorWjdr.DomainModel;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Talent
{
    public partial class TalentItem
    {
        [Parameter]
        public TalentDto Talent { get; set; } = null!;
    }
}
