namespace BlazorWjdr.Models
{
    /*
    public class RootCampagneDto
    {
        private readonly Dictionary<int, UserDto> _users;
        private readonly Dictionary<int, CampagneDto> _campagnes;
        private readonly Dictionary<int, TeamDto> _teams;
    }
    */
    public class UserDto
    {
        public int Id { get; init; }
        public string Email { get; init; } = null!;
        public string Pseudo { get; init; } = null!;
    }

    public class CampagneDto
    {
        public int Id { get; init; }
        public string Titre { get; init; } = null!;
        public UserDto Mj { get; init; } = null!;
    }

    public class TeamDto
    {
        public int Id { get; init; }
        public string Nom { get; init; } = null!;
    }

    public class FactDto
    {
        public int Tri { get; init; }
        public UserDto[] Pjs { get; init; } = null!;
        public string Fact { get; init; } = null!;
    }

    public class SeanceDto
    {
        public TeamDto? Team { get; init; }
        public CampagneDto Campagne { get; init; } = null!;
        public string Quand { get; init; } = null!;
        public int Acte { get; init; }
        public int Duree { get; init; }
        public string Titre { get; init; } = null!;
        public int Xp { get; init; }
        public string? XpComment { get; init; }
        public string Resume { get; init; } = null!;
        public UserDto[] Pjs { get; init; } = null!;
        public FactDto[]? Facts { get; init; }
    }
}