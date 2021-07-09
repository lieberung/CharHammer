using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Competence
{
    public partial class CompetenceItemList
    {
        [Parameter] public string Title { get; set; } = null!;
        [Parameter] public CompetenceDto[] Items { get; set; } = null!;
        [Parameter] public CompetenceDto[][] ItemsChoix { get; set; } = System.Array.Empty<CompetenceDto[]>();
    }
}