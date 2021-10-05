using System.Collections.Generic;

namespace BlazorWjdr.Models
{
    public class RootCampagneDto
    {
        private readonly Dictionary<int, UserDto> _users;
        private readonly Dictionary<int, CampagneDto> _campagnes;
        private readonly Dictionary<int, TeamDto> _teams;
    }
    
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Pseudo { get; set; } = null!;
    }

    public class CampagneDto
    {
        public int Id { get; set; }
        public string Titre { get; set; } = null!;
        public int Mj { get; set; }
    }

    public class TeamDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
    }

    public class FactDto
    {
        public int Tri { get; set; }
        public UserDto[] Pjs { get; set; }
        public string Fact { get; set; } = null!;
    }

    public class SeanceDto
    {
        public TeamDto? Team { get; set; }
        public CampagneDto Campagne { get; set; }
        public string Quand { get; set; } = null!;
        public int Acte { get; set; }
        public int Duree { get; set; }
        public string Titre { get; set; } = null!;
        public int Xp { get; set; }
        public string XpComment { get; set; }
        public string Resume { get; set; }
        public UserDto[] Pjs { get; set; }
        public FactDto[]? Facts { get; set; }
    }
}