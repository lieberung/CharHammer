using BlazorWjdr.DomainModel;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Profil
{
    public partial class PlanDeCarriere
    {
        [Parameter]
        public PlanDeCarriereDto Plan { get; set; } = null!;
    }
}
