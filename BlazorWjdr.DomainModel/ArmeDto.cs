﻿using System.Linq;

namespace BlazorWjdr.Models
{
    using System.Collections.Generic;

    public class ArmeDto
    {
        public int Id { get; init; }
        public List<TalentDto> TalentsDeMaitrise { get; init; } = null!;
        public List<ArmeAttributDto> Attributs { get; init; } = null!;
        public List<ArmeAttributDto> Groupes { get; init; } = null!;
        public string Nom { get; init; } = null!;
        public string Degats { get; init; } = null!;
        public string Portee { get; init; } = null!;
        public string Rechargement { get; init; } = null!;
        public string Encombrement { get; init; } = null!;
        public string Prix { get; init; } = null!;
        public string Disponibilite { get; init; } = null!;
        public string Description { get; init; } = null!;

        public bool EstUneArmeDeCaC => Degats.StartsWith("BF") && Groupes.All(g => g.Nom != "De jet");
        public bool EstUneArmeDeTir => Portee != "";
        public bool EstUneMunition => Groupes.Any(g => g.Nom == "Munitions");
    }
}
