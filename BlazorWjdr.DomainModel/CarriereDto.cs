namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

    public class CarriereDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string Image { get; init; } = null!;
        public bool EstUneCarriereAvancee { get; init; }
        public string Restriction { get; init; } = null!;
        public string Source { get; init; } = null!;
        public int[] DebouchesIds { get; init; } = null!;
        public int? SourceId { get; init; }
        public bool Complete { get; init; }
        public string Dotations { get; init; } = null!;
        public int? CarriereMereId { get; init; }

        public PlanDeCarriereDto PlanDeCarriere { get; set; } = null!;
        
        public List<TalentDto> Talents { get; set; } = null!;
        public List<CompetenceDto> Competences { get; set; } = null!;

        public List<TalentDto[]> ChoixTalents { get; set; } = null!;
        public List<CompetenceDto[]> ChoixCompetences { get; set; } = null!;

        public CarriereDto? CarriereMere { get; set; }
        public List<CarriereDto> Debouches = new List<CarriereDto>();
        public List<CarriereDto> Filieres = new List<CarriereDto>();
    }
}
