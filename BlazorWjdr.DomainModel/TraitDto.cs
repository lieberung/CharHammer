using System.Collections.Generic;

namespace BlazorWjdr.Models
{
    public class TraitDto
    {
        public int Id { get; init; }
        public int Severite { get; init; }
        public string Groupe { get; init; } = null!;
        public string Nom { get; init; } = null!;
        public string Guerison { get; init; } = null!;
        public string Description { get; init; } = null!;
        public bool? Contagieux { get; init; }

    }
}