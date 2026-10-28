namespace CharHammer.Models;

public record BestioleDto(
    int Id, string DateDeCreation, int UserId, string Joueur, string Nom, string NomCourt,
    RaceDto Race,
    ProfilDto? ProfilInitial, ProfilDto ProfilActuel,
    string Description, string Histoire, string Notes, string Psychologie, IReadOnlyList<string> Ambitions,
    IReadOnlyList<CarriereDto> CheminementPro,
    IReadOnlyList<AptitudeAcquiseDto> AptitudesAcquises, IReadOnlyList<AptitudeAcquiseDto> AptitudesOptionnels,
    IReadOnlyList<LieuDto> Origines,
    IReadOnlyList<string> MembreDe,
    int? Poids, int? Taille, int? Age, int? Sexe, string? Cheveux, string? Yeux, int MainDirectrice,
    IReadOnlyList<ArmeDto> Armes, IReadOnlyList<ArmureDto> Armures, IReadOnlyList<EquipementDto> Equipement,
    IReadOnlyList<SortilegeDto> Sorts,
    CarriereDto? CarriereDuPere, CarriereDto? CarriereDeLaMere, string? FreresEtSoeurs,
    int? SigneAstralId,
    bool Mort, bool Masquer,
    int XpActuel, int XpTotal
) : ISearchable
{
    public bool EstUnPersonnage => CheminementPro.Any();
    public bool EstUnPersonnageJoueur => UserId != 0 && UserId != 999;
    public bool EstUnPersonnagePretire => EstUnPersonnage && UserId == 999;
    public bool EstUnPersonnageNonJoueur => EstUnPersonnage && UserId == 0;
    public bool EstUnArchetype => EstUnPersonnageNonJoueur && Nom == "Archétype";
    public bool EstUneCreature => !EstUnPersonnage;

    public IReadOnlyList<AptitudeAcquiseDto> Competences { get; } = AptitudesAcquises.Where(a => a.Aptitude.EstUneCompetence).ToArray();
    public IReadOnlyList<AptitudeAcquiseDto> Talents { get; } = AptitudesAcquises.Where(a => a.Aptitude.EstUnTalent).ToArray();
    public IReadOnlyList<AptitudeAcquiseDto> Traits { get; } = AptitudesAcquises.Where(a => a.Aptitude.EstUnTrait).ToArray();

    public ProtectionsDto Protections { get; } = CalculerProtections(Armures, AptitudesAcquises);

    private static ProtectionsDto CalculerProtections(IReadOnlyList<ArmureDto> armures, IReadOnlyList<AptitudeAcquiseDto> aptitudesAcquises)
    {
        var synthese = new ProtectionsDto();
        foreach (var armure in armures.Where(a => a.Pa != "" && a.Pa != "0"))
        {
            var zones = armure.Zones.ToLower();
            if (!int.TryParse(armure.Pa, out var pa))
                continue;
            if (zones.Contains("toutes") || zones.Contains("tête"))
                synthese.Tete += pa;
            if (zones.Contains("toutes") || zones.Contains("bras"))
                synthese.Bras += pa;
            if (zones.Contains("toutes") || zones.Contains("torse"))
                synthese.Torse += pa;
            if (zones.Contains("toutes") || zones.Contains("jambes"))
                synthese.Jambes += pa;
        }
        var armureNaturelle = aptitudesAcquises.SingleOrDefault(aa => aa.Aptitude.Id == 4001);
        if (armureNaturelle is not null)
        {
            synthese.Tete += armureNaturelle.Niveau;
            synthese.Bras += armureNaturelle.Niveau;
            synthese.Torse += armureNaturelle.Niveau;
            synthese.Jambes += armureNaturelle.Niveau;
        }
        return synthese;
    }

    public CarriereDto? CarriereActuelle => CheminementPro.LastOrDefault();

    public AptitudeDto? Gabarit;
    public int Blessures;
    public string BlessuresDetailDuCalcul = "";
    public string BlessuresFormuleDeCalcul = "";

    public string EquipementDeCarrieres { get; } = string.Join(", ", CheminementPro.SelectMany(c => c.Dotations.Split(", ")).Distinct().OrderBy(s => s));

    public class ProtectionsDto
    {
        public int Tete { get; set; }
        public int Bras { get; set; }
        public int Torse { get; set; }
        public int Jambes { get; set; }

        private int Total => Tete + Bras + Torse + Jambes;
        public bool Aucune => Total == 0;
        public override string ToString()
        {
            if (Aucune) return "";

            var toStr = new List<string>();
            if (Tete != 0)
                toStr.Add($"Tête {Tete}");
            if (Bras != 0)
                toStr.Add($"Bras {Bras}");
            if (Torse != 0)
                toStr.Add($"Torse {Torse}");
            if (Jambes != 0)
                toStr.Add($"Jambes {Jambes}");

            if (Tete == Bras && Tete == Torse && Tete == Jambes)
                return $"Toutes les zones {Tete}";

            return string.Join(", ", toStr);
        }
    }
}
