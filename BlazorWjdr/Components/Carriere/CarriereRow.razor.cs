using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Carriere
{
    public partial class CarriereRow
    {
        [Parameter]
        public CarriereDto Item { get; set; } = null!;
        
        private bool AfficherSousElements { get; set; } = false;
        private bool AfficherPlanDeCarriere { get; set; }
        private bool AfficherCompetencesEtTalents { get; set; }
        private bool AfficherFilieresEtDebouches { get; set; }
    }
}
