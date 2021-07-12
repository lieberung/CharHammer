using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWjdr.Models;
using BlazorWjdr.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorWjdr.Components
{
    public partial class RechercheGlobale
    {
        private IEnumerable<CarriereDto> AllCarrieres { get; set; } = new List<CarriereDto>();
        private IEnumerable<CompetenceDto> AllCompetences { get; set; } = new List<CompetenceDto>();
        private IEnumerable<TalentDto> AllTalents { get; set; } = new List<TalentDto>();

        [Parameter]
        public string Text { get; set; } = "";
        
        private string _searchText = "";

        [Inject] private CarrieresService CarrieresService { get; set; } = null!;
        [Inject] private CompetencesEtTalentsService CompetencesEtTalentsService { get; set; } = null!;
        
        protected override async Task OnInitializedAsync()
        {
            AllCarrieres = await CarrieresService.ItemsCarrieres();
            AllCompetences = await CompetencesEtTalentsService.ItemsCompetences();
            AllTalents = await CompetencesEtTalentsService.ItemsTalents();
        }

        protected override void OnParametersSet()
        {
            _searchText = Text;
            base.OnParametersSet();
        }

        private CarriereDto[] FilteredCarrieres => _searchText.Length < 3
            ?  new CarriereDto[0]
            : AllCarrieres.Where(c => c.Nom.ToLower().Contains(_searchText.ToLower())).ToArray();
        
        private CompetenceDto[] FilteredCompetences => _searchText.Length < 3
            ?  new CompetenceDto[0]
            : AllCompetences.Where(c => c.Nom.ToLower().Contains(_searchText.ToLower())).ToArray();

        private TalentDto[] FilteredTalents => _searchText.Length < 3
            ?  new TalentDto[0]
            : AllTalents.Where(c => c.Nom.ToLower().Contains(_searchText.ToLower())).ToArray();
    }
}