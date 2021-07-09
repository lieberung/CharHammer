using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Carriere
{
    public partial class CarriereRow
    {
        [Parameter]
        public CarriereDto Carriere { get; set; } = null!;

        private bool AfficherPlanDeCarriere { get; set; }
        private bool AfficherCompetencesEtTalents { get; set; }
    }
}
