﻿namespace BlazorWjdr.DomainModel
{
    using System.Collections.Generic;
    using System.Linq;

    public class CompetenceDto
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = null!;
        public string Resume { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool EstUneCompetenceDeBase { get; set; }
        public string CaracteristiqueAssociee { get; set; } = null!;
        public List<TalentDto> TalentsLies { get; set; } = new List<TalentDto>();
        public int? CompetenceMereId { get; set; }
        public CompetenceDto? CompetenceParente { get; set; }
        public string? Specialisation { get; set; }
        public bool Ignore { get; set; }

        public string TalentsLiesToString => TalentsLies.Any() ?
            string.Join(", ", TalentsLies
                .Where(t => t.Ignore == false)
                .OrderBy(t => t.Libelle).ThenBy(t => t.Specialisation)
                .Select(t => t.ToString())
                .ToArray())
            : "";

        public override string ToString() => $"{Libelle}{(Specialisation != null ? $" ({Specialisation})" : "")}";
    }
}
