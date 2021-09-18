using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

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
        public string Chef { get; set; } = null!;
        public string Fetes { get; set; } = null!;
        public string LivresSaints { get; set; } = null!;
        public string Intro { get; set; } = null!;
        public string Penitences { get; set; } = null!;
        public string Culte { get; set; } = null!;
        public string Dogme { get; set; } = null!;
        public string Initiation { get; set; } = null!;
        public string Pretrise { get; set; } = null!;
        public string Cultistes { get; set; } = null!;
        public string Structure { get; set; } = null!;
        public AptitudesAssocieesDto Aptitudes { get; set; } = null!;
        public List<RegleAssocieeDto> Regles { get; set; } = null!;
        public List<string> Commandements { get; set; } = null!;
        public List<SecteDto> Sectes { get; set; } = null!;
        public List<string> Ambiance { get; set; } = null!;
        public List<TempleDto> Temples { get; set; } = null!;
        public List<PersonnaliteDto> Personnalites { get; set; } = null!;
        public List<CulteDto> Ordres { get; set; } = null!;
    }
    
    public class CulteDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public List<AptitudeDto> Aptitudes { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Mineur { get; set; }
        public string Ambiance { get; set; } = null!;
    }
    
    public class AptitudesAssocieesDto
    {
        public List<AptitudeDto> Inities { get; init; } = null!;
        public List<AptitudeDto> PretesSansOrdre { get; init; } = null!;
    }

    public class RegleAssocieeDto
    {
        public string Titre { get; init; } = null!;
        public string Description { get; init; } = null!;
    }

    public class SecteDto
    {
        public string Nom { get; init; } = null!;
        public string Description { get; init; } = null!;
    }

    public class TempleDto
    {
        public string Nom { get; init; } = null!;
        public string Description { get; init; } = null!;
    }

    public class PersonnaliteDto
    {
        public string Nom { get; init; } = null!;
        public string Description { get; init; } = null!;
    }
}
