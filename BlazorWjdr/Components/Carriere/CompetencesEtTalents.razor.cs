using BlazorWjdr.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorWjdr.Components.Carriere
{
    public partial class CompetencesEtTalents
    {
        [Parameter]
        public CarriereDto Carriere { get; set; } = null!;

        private CompetenceDto[] _competences = null!;
        private TalentDto[] _talents = null!;
        
        private CompetenceDto[][] _competencesChoix = null!;
        private TalentDto[][] _talentsChoix = null!;

        protected override void OnInitialized()
        {
            _competences = Carriere.Competences.ToArray();
            _talents = Carriere.Talents.ToArray();
            _competencesChoix = Carriere.ChoixCompetences.ToArray();
            _talentsChoix = Carriere.ChoixTalents.ToArray();
        }
    }
}
