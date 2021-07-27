namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

    public class ArmeDto
    {
        public int Id { get; set; }
        public List<TalentDto> TalentsDeMaitrise { get; set; }
        public List<ArmeAttributDto> Attributs { get; set; }
        public string Groupe { get; set; }
        public string Nom { get; set; }
        public string Degats { get; set; }
        public string Portee { get; set; }
        public string Rechargement { get; set; }
        public string Encombrement { get; set; }
        public string Prix { get; set; }
        public string Disponibilite { get; set; }
        public string Description { get; set; }
    }
}
