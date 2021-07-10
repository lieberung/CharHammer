using System.Threading.Tasks;
using BlazorWjdr.Models;
using BlazorWjdr.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components.Competence
{
    public partial class CompetenceRow
    {
        [Inject] private CarrieresService CarrieresService { get; set; } = null!;

        [Parameter] public CompetenceDto Item { get; set; } = null!;
        private bool AfficherSousElements { get; set; } = false;
        private bool AfficherCarrieres { get; set; } = false;
        
        private CarriereDto[] _carrieresLiees = null!;

        protected override async Task OnInitializedAsync()
        {
            _carrieresLiees = await CarrieresService.GetCarrieresProposant(Item);
        }
    }
}