namespace BlazorWjdr.DomainModel
{
    using System.Collections.Generic;

    public class CarriereDto
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public bool EstUneCarriereAvancee { get; set; }
        public string Restriction { get; set; } = null!;
        public string Source { get; set; } = null!;
        public int[] DebouchesIds { get; set; } = null!;
        public int? SourceId { get; set; }
        public bool Complete { get; set; }
        public string Dotations { get; set; } = null!;
        public int? CarriereMereId { get; set; }

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
