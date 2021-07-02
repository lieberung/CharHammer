using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Profil
{
    public partial class ProfilRowBonus
    {
        [Parameter]
        public string LeftTitle { get; set; } = "";
        [Parameter]
        public PlanDeCarriereDto PlanDeCarriere { get; set; } = null!;
    }
}
