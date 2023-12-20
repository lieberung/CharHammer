namespace CharHammer.Models;

public record BestioleDto(
    int Id, string DateDeCreation, int UserId, string Joueur, string Nom, string NomCourt,
    RaceDto Race,
    ProfilDto? ProfilInitial, ProfilDto ProfilActuel,
    string Description, string Histoire, string Notes, string Psychologie, IEnumerable<string> Ambitions,
    IEnumerable<CarriereDto> CheminementPro,
    IEnumerable<AptitudeAcquiseDto> AptitudesAcquises, IEnumerable<AptitudeAcquiseDto> AptitudesOptionnels,
    IEnumerable<LieuDto> Origines,
    IEnumerable<string> MembreDe,
    int? Poids, int? Taille, int? Age, int? Sexe, string? Cheveux, string? Yeux, int MainDirectrice,
    IEnumerable<ArmeDto> Armes, IEnumerable<ArmureDto> Armures, IEnumerable<EquipementDto> Equipement,
    IEnumerable<SortilegeDto> Sorts,
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

    public IEnumerable<AptitudeAcquiseDto> Competences => AptitudesAcquises.Where(a => a.Aptitude.EstUneCompetence);
    public IEnumerable<AptitudeAcquiseDto> Talents => AptitudesAcquises.Where(a => a.Aptitude.EstUnTalent);
    public IEnumerable<AptitudeAcquiseDto> Traits => AptitudesAcquises.Where(a => a.Aptitude.EstUnTrait);

    public ProtectionsDto Protections
    {
        get
        {
            var synthese = new ProtectionsDto();
            foreach (var armure in Armures.Where(a => a.Pa != "" && a.Pa != "0"))
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
            var armureNaturelle = AptitudesAcquises.SingleOrDefault(aa => aa.Aptitude.Id == 4001);
            if (armureNaturelle is not null)
            {
                synthese.Tete += armureNaturelle.Niveau;
                synthese.Bras += armureNaturelle.Niveau;
                synthese.Torse += armureNaturelle.Niveau;
                synthese.Jambes += armureNaturelle.Niveau;
            }
            return synthese;
        }
    }

    public CarriereDto? CarriereActuelle => CheminementPro.LastOrDefault();

    public AptitudeDto? Gabarit;
    public int Blessures;
    public string BlessuresDetailDuCalcul = "";
    public string BlessuresFormuleDeCalcul = "";

    public string EquipementDeCarrieres => string.Join(", ", CheminementPro.SelectMany(c => c.Dotations.Split(", ")).Distinct().OrderBy(s => s));

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
