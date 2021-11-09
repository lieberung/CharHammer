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
    }

    public class SeanceDto
    {
        public string Quand { get; init; } = null!;
        public int Acte { get; init; }
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