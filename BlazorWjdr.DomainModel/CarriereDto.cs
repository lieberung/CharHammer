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
        public string Source { get; set; } = null!;
        public int[] DebouchesIds { get; init; } = null!;
        public bool Complete { get; init; }
        public string Dotations { get; init; } = null!;
        public int? CarriereMereId { get; init; }

        public PlanDeCarriereDto PlanDeCarriere { get; init; } = null!;
        
        public List<TalentDto> Talents { get; set; } = null!;
        public List<CompetenceDto> Competences { get; init; } = null!;

        public List<TalentDto[]> ChoixTalents { get; init; } = null!;
        public List<CompetenceDto[]> ChoixCompetences { get; init; } = null!;

        public CarriereDto? CarriereMere { get; set; }
        public List<CarriereDto> Debouches = new List<CarriereDto>();
        public List<CarriereDto> Filieres = new List<CarriereDto>();

        public ReferenceDto? SourceLivre { get; init; }
    }
}
