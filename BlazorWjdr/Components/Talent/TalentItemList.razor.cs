using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Talent
{
    public partial class TalentItemList
    {
        [Parameter] public string Title { get; set; } = null!;
        [Parameter] public TalentDto[] Items { get; set; } = null!;
        [Parameter] public TalentDto[][] ItemsChoix { get; set; } = System.Array.Empty<TalentDto[]>();
    }
}