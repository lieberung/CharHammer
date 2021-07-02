using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Competence
{
    public partial class CompetenceItem
    {
        [Parameter]
        public CompetenceDto Competence { get; set; } = null!;
    }
}
