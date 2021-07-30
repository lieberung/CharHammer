namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

    public class ArmeDto
    {
        public int Id { get; set; }
        public List<TalentDto> TalentsDeMaitrise { get; set; } = null!;
        public List<ArmeAttributDto> Attributs { get; set; } = null!;
        public List<ArmeAttributDto> Groupes { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Degats { get; set; } = null!;
        public string Portee { get; set; } = null!;
        public string Rechargement { get; set; } = null!;
        public string Encombrement { get; set; } = null!;
        public string Prix { get; set; } = null!;
        public string Disponibilite { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
