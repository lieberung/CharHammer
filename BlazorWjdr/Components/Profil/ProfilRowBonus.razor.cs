using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Profil
{
    public partial class ProfilRowBonus
    {
        [Parameter]
        public PlanDeCarriereDto PlanDeCarriere { get; set; } = null!;
    }
}
