namespace BlazorWjdr.Pages
{
    using BlazorWjdr.Models;
    using BlazorWjdr.Services;
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    public partial class CarriereDetail
    {
        [Parameter]
        public string CarriereId { get; set; } = null!;

        public CarriereDto Carriere { get; set; } = null!;

        [Inject]
        public CarrieresService CarrieresService { get; set; } = null!;

        protected override Task OnInitializedAsync()
        {
            Carriere = CarrieresService.GetCarriere(int.Parse(CarriereId));
            return base.OnInitializedAsync();
        }
    }
}
