using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Carriere
{
    public partial class CarriereRow
    {
        [Parameter]
        public CarriereDto Carriere { get; set; } = null!;

        public bool AfficherPlanDeCarriere { get; set; }
        public bool AfficherCompetencesEtTalents { get; set; }
    }
}
