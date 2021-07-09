using System.Threading.Tasks;
using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Competence
{
    public partial class CompetenceRow
    {
        [Parameter] public CompetenceDto Item { get; set; } = null!;
        private bool AfficherSousElements { get; set; } = false;
    }
}