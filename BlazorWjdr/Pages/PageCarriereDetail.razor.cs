namespace BlazorWjdr.Pages
{
    using Models;
    using Services;
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    public partial class PageCarriereDetail
    {
        [Parameter]
        public string CarriereId { get; set; } = null!;

        private CarriereDto Carriere { get; set; } = null!;
        private bool _afficherGallerie;

        [Inject]
        public CarrieresService CarrieresService { get; set; } = null!;

        protected override Task OnParametersSetAsync()
        {
            Carriere = CarrieresService.GetCarriere(int.Parse(CarriereId));

            return base.OnParametersSetAsync();
        }
    }
}
