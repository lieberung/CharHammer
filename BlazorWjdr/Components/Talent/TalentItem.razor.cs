using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Talent
{
    public partial class TalentItem
    {
        [Parameter]
        public TalentDto Item { get; set; } = null!;
    }
}
