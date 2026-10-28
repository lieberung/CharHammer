namespace CharHammer.Services;

public class BestiolesService
{
    private readonly IReadOnlyDictionary<int, BestioleDto> _data;

    // Listes calculées une fois ; les variantes « mode dieu » et « joueurs » sont précalculées toutes les deux.
    private readonly IReadOnlyList<BestioleDto> _pnjsTous;
    private readonly IReadOnlyList<BestioleDto> _pnjsVisibles;
    private readonly IReadOnlyDictionary<string, IReadOnlyList<BestioleDto>> _regroupeesToutes;
    private readonly IReadOnlyDictionary<string, IReadOnlyList<BestioleDto>> _regroupeesVisibles;
    private readonly (BestioleDto Bestiole, string Nom)[] _nomsPourRecherche;

    public BestiolesService(IReadOnlyDictionary<int, BestioleDto> data)
    {
        _data = data;
        AllBestioles = data.Values.OrderBy(b => b.Nom).ToArray();
        AllArchetypes = AllBestioles.Where(b => b.EstUnArchetype).ToArray();
        AllPjs = data.Values.Where(b => b.EstUnPersonnageJoueur).ToArray();
        AllPretires = AllBestioles.Where(b => b.EstUnPersonnagePretire).ToArray();
        _nomsPourRecherche = AllBestioles.Select(b => (b, GenericService.NettoyerPourRecherche(b.Nom))).ToArray();

        _pnjsTous = AllBestioles.Where(b => b is { EstUnPersonnageNonJoueur: true, EstUnArchetype: false }).ToArray();
        _pnjsVisibles = _pnjsTous.Where(b => b.Masquer == false).ToArray();

        _regroupeesToutes = Regrouper(jeSuisDieu: true);
        _regroupeesVisibles = Regrouper(jeSuisDieu: false);
    }

    private IReadOnlyList<BestioleDto> AllBestioles { get; }

    public IReadOnlyList<BestioleDto> AllArchetypes { get; }
    public IReadOnlyList<BestioleDto> AllPnjs(bool godMode) => godMode ? _pnjsTous : _pnjsVisibles;
    public IReadOnlyList<BestioleDto> AllPjs { get; }
    public IReadOnlyList<BestioleDto> AllPretires { get; }

    public IReadOnlyDictionary<string, IReadOnlyList<BestioleDto>> BestiolesRegroupees(bool jeSuisDieu)
        => jeSuisDieu ? _regroupeesToutes : _regroupeesVisibles;

    /// <summary>Groupes des créatures (hors personnages) visibles ; chaque groupe liste tous ses membres, masqués compris.</summary>
    private IReadOnlyDictionary<string, IReadOnlyList<BestioleDto>> Regrouper(bool jeSuisDieu)
    {
        var creatures = AllBestioles.Where(b => b.EstUnPersonnage == false).ToArray();
        return creatures
            .Where(p => p.Masquer == false || jeSuisDieu)
            .SelectMany(b => b.MembreDe)
            .Distinct()
            .OrderBy(g => g)
            .ToDictionary(
                groupe => groupe,
                IReadOnlyList<BestioleDto> (groupe) => creatures.Where(b => b.MembreDe.Contains(groupe)).ToArray());
    }

    public BestioleDto GetBestiole(int id) => _data[id];

    public static AptitudeDto? GetGabarit(BestioleDto bestiole)
    {
        return bestiole.AptitudesAcquises.Select(aa => aa.Aptitude)
            .FirstOrDefault(a => a.Parent?.Id == AptitudesService.AptitudeGroupeGabarit);
    }
    
    public IEnumerable<BestioleDto> Recherche(string searchText)
    {
        searchText = GenericService.NettoyerPourRecherche(searchText);
        return _nomsPourRecherche
            .Where(x => x.Nom.Contains(searchText))
            .Select(x => x.Bestiole);
    }

    public static int CalculBlessures(AptitudeDto gabarit, ProfilDto profil, bool durACuir)
    {
        var blessures = gabarit.Id switch
        {
            AptitudesService.TraitGabaritMinusculeId => 1,
            AptitudesService.TraitGabaritToutPetitId => profil.Be,
            AptitudesService.TraitGabaritPetitId => 2 * profil.Be + profil.Bfm,
            AptitudesService.TraitGabaritMoyenId => profil.Bf + 2 * profil.Be + profil.Bfm,
            AptitudesService.TraitGabaritLargeId => 2 * (profil.Bf + 2 * profil.Be + profil.Bfm),
            AptitudesService.TraitGabaritEnormeId => 4 * (profil.Bf + 2 * profil.Be + profil.Bfm),
            AptitudesService.TraitGabaritMonstrueuxId => 8 * (profil.Bf + 2 * profil.Be + profil.Bfm),
            _ => throw new Exception("Gabarit invalide pour calcul des Blessures")
        };
        if (durACuir)
            blessures += profil.Be;
        return blessures;
    }

    public static string GetBlessuresDetailDuCalcul(AptitudeDto gabarit, ProfilDto profil, bool durACuir)
    {
        var blessures = gabarit.Id switch
        {
            AptitudesService.TraitGabaritMinusculeId => "",
            AptitudesService.TraitGabaritToutPetitId => "",
            AptitudesService.TraitGabaritPetitId => $"(2 * {profil.Be}) + {profil.Bfm}",
            AptitudesService.TraitGabaritMoyenId => $"{profil.Bf} + (2 * {profil.Be}) + {profil.Bfm}",
            AptitudesService.TraitGabaritLargeId => $"2 * ({profil.Bf} + (2 * {profil.Be}) + {profil.Bfm})",
            AptitudesService.TraitGabaritEnormeId => $"4 * ({profil.Bf} + (2 * {profil.Be}) + {profil.Bfm})",
            AptitudesService.TraitGabaritMonstrueuxId => $"8 * ({profil.Bf} + (2 * {profil.Be}) + {profil.Bfm})",
            _ => throw new Exception("Gabarit invalide pour détail du calcul des Blessures")
        };
        if (durACuir)
            blessures += $" + {profil.Be}*";
        return blessures;
    }

    public static string GetBlessuresFormuleDeCalcul(AptitudeDto gabarit, bool durACuir)
    {
        var blessures = gabarit.Id switch
        {
            AptitudesService.TraitGabaritMinusculeId => "(gabarit minuscule)",
            AptitudesService.TraitGabaritToutPetitId => "BE",
            AptitudesService.TraitGabaritPetitId => "(2 * BE) + BFM",
            AptitudesService.TraitGabaritMoyenId => "BF + (2 * BE) + BFM",
            AptitudesService.TraitGabaritLargeId => "2 * (BF + (2 * BE) + BFM)",
            AptitudesService.TraitGabaritEnormeId => "4 * (BF + (2 * BE) + BFM)",
            AptitudesService.TraitGabaritMonstrueuxId => "8 * (BF + (2 * BE) + BFM)",
            _ => throw new Exception("Gabarit invalide pour formule de calcul des Blessures")
        };
        if (durACuir)
            blessures += " + BE (dur à cuir)";
        return blessures;
    }

    /// <summary>Lance l'initiative une seule fois : le résultat est matérialisé pour que les dés ne soient pas relancés à chaque énumération.</summary>
    public static IReadOnlyList<CombattantEnJeu> InitiativeDeCombat(IEnumerable<CombattantDto> combattants)
    {
        return combattants
            .Select(JetDInitiativeDeCombat)
            .OrderByDescending(idc => idc.JetDInitiative)
            .ThenByDescending(idc => idc.Combattant.ProfilActuel.I)
            .ToArray();
    }

    private static CombattantEnJeu JetDInitiativeDeCombat(CombattantDto combattant)
    {
        var profil = combattant.Combattant.ProfilActuel;
        var initiative = profil.BonusDInitiative * 2 + profil.BonusDAgilite;
        var detail = $"2 x {profil.BonusDInitiative} (BI) + {profil.BonusDAgilite} (BAg)";
        
        var reflexes = combattant.Combattant.AptitudesAcquises.SingleOrDefault(a => a.Aptitude.Id == AptitudesService.TalentReflexesDeCombatId)?.Niveau ?? 0;
        if (reflexes != 0)
        {
            initiative += reflexes * 2;
            detail += $" + {reflexes * 2} (RdC)";
        }
        
        var dice = GenericService.RollDice(10);
        initiative += dice;
        detail += $" + {dice} (1d10)";

        return new CombattantEnJeu(combattant, initiative, detail);
    }
}
