using BlazorWjdr.Models;
using BlazorWjdr.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Aptitude
{
    public partial class AptitudeRow
    {
        [Inject] private CarrieresService CarrieresService { get; set; } = null!;

        [Parameter] public AptitudeDto Item { get; set; } = null!;
        private bool AfficherSousElements { get; set; }
        private bool AfficherCarrieres { get; set; }
        
        private CarriereDto[] _carrieresLiees = null!;

        protected override void OnInitialized()
        {
            _carrieresLiees = CarrieresService.GetCarrieresProposant(Item);
        }
    }
}