using System.Collections.Generic;

namespace BlazorWjdr.Models
{
    public class DieuDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
        public string Domaines { get; init; } = null!;
        public string Fideles { get; init; } = null!;
        public string Symboles { get; init; } = null!;
        public string Histoire { get; init; } = null!;
        public string Commentaire { get; init; } = null!;

        public int? PatronId { get; init; }
        public DieuDto? Patron { get; set; }
        
        public LieuDto? Siege { get; set; }
        public AptitudesAssocieesDto Aptitudes { get; set; }
        public string Chef { get; set; }
        public string Fetes { get; set; }
        public string LivresSaints { get; set; }
        public string Intro { get; set; }
        public string Penitences { get; set; }
        public string Culte { get; set; }
        public List<RegleAssocieeDto> Regles { get; set; }
        public string Dogme { get; set; }
        public string Initiation { get; set; }
        public string Pretrise { get; set; }
        public List<string> Commandements { get; set; }
        public string Cultistes { get; set; }
        public string Structure { get; set; }
        public List<SecteDto> Sectes { get; set; }
        public List<string> Ambiance { get; set; }
        public List<TempleDto> Temples { get; set; }
        public List<PersonnaliteDto> Personnalites { get; set; }
    }
    
    public class AptitudesAssocieesDto
    {
        public List<AptitudeDto> Inities { get; init; }
        public List<AptitudeDto> PretesSansOrdre { get; init; }
    }

    public class RegleAssocieeDto
    {
        public string Titre { get; init; }
        public string Description { get; init; }
    }

    public class SecteDto
    {
        public string Nom { get; init; }
        public string Description { get; init; }
    }

    public class TempleDto
    {
        public string Nom { get; init; }
        public string Description { get; init; }
    }

    public class PersonnaliteDto
    {
        public string Nom { get; init; }
        public string Description { get; init; }
    }
}
