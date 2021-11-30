using System.Linq;

namespace BlazorWjdr.Models
{
    public class UserDto
    {
        public int Id { get; init; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Email { get; init; } = null!;
        public string Pseudo { get; init; } = null!;
    }

    public class TeamDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
    }

    public class CampagneDto
    {
        public int Id { get; init; }
        public string Titre { get; init; } = null!;
        public UserDto Mj { get; init; } = null!;
        public TeamDto Team { get; init; } = null!;
        public SeanceDto[] Seances { get; init; } = null!;
        public ContactDeCampagneDto[] Contacts { get; init; } = null!;

        public SeanceDto SeancePrecedente() => Seances.Where(s => s.Secret == false).OrderByDescending(s => s.Quand).First();
        public SeanceDto SeanceActuelle() => Seances.Where(s => s.Secret).OrderBy(s => s.Quand).First();
        
        public SeanceDto[] SeancesPourLActe(int acte, bool godMode) => Seances
                .Where(s => s.Acte == acte && (godMode || s.Secret == false))
                .OrderBy(s => s.Quand)
                .ToArray();
    }

    public class SeanceDto
    {
        public string Quand { get; init; } = null!;
        public bool Secret { get; set; }
        public int Acte { get; init; }
        public string Debut { get; init; } = null!;
        public int Duree { get; init; }
        public string Titre { get; init; } = null!;
        public ScenarioDto? Scenario { get; init; }
        public int Xp { get; init; }
        public string? XpComment { get; init; }
        public string Resume { get; init; } = null!;
        public LieuDto[] Lieux { get; init; } = null!;
        public BestioleDto[] Pjs { get; init; } = null!;
        public FactDto[] Facts { get; init; } = null!;
        public RencontreDto[] Rencontres { get; init; } = null!;
    }

    public class ContactDeCampagneDto
    {
        public BestioleDto Pnj { get; set; } = null!;
        public LieuDto LieuDeRencontre { get; set; } = null!;
        public LieuDto? LieuDeResidence { get; set; }
        public CarriereDto[] ProposeLesCarrieres { get; set; } = null!;
        public string[] Notes { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    
    public class FactDto
    {
        public int Tri { get; init; }
        public BestioleDto[] Pjs { get; init; } = null!;
        public string Fact { get; init; } = null!;
    }
    
    public class RencontreDto
    {
        public string Groupe { get; init; } = null!;
        public CombattantDto[] Allies { get; init; } = null!;
        public CombattantDto[] Ennemis { get; init; } = null!;
    }
}