namespace BlazorWjdr.Services;

using Models;
using System.Collections.Generic;
using System.Linq;

public class ArmesService
{
    private readonly Dictionary<int, ArmeAttributDto> _cacheArmeAttribut;

    private readonly Dictionary<int, ArmeDto> _cacheArme;
    private readonly Dictionary<int, ArmureDto> _cacheArmure;
    private readonly Dictionary<int, EquipementDto> _cacheEquipement;

    private Dictionary<string, List<ArmeDto>>? _armesDeContactPourTable;
    private Dictionary<string, List<ArmeDto>>? _armesADistancePourTable;

    public ArmesService(
        Dictionary<int, ArmeAttributDto> dataArmesAttributs, 
        Dictionary<int, ArmeDto> dataArmes,
        Dictionary<int, ArmureDto> dataArmures,
        Dictionary<int, EquipementDto> dataEquipements)
    {
        _cacheArmeAttribut = dataArmesAttributs;
        _cacheArme = dataArmes;
        _cacheArmure = dataArmures;
        _cacheEquipement = dataEquipements;
    }

    public List<ArmeAttributDto> AllGroupesDArmes => _cacheArmeAttribut.Values.Where(a => a.Type == "groupe").OrderBy(g => g.Nom).ToList();

    public List<ArmeDto> AllArmes => _cacheArme.Values.ToList();
    public IEnumerable<ArmureDto> AllArmures => _cacheArmure.Values.ToList();
    public IEnumerable<EquipementDto> AllEquipements => _cacheEquipement.Values.ToList();
    
    private ArmeAttributDto GetAttributDArme(int id) => _cacheArmeAttribut[id];

    public List<ArmeDto> GetArmesDeMaitrise(AptitudeDto maitrise) =>
        AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == maitrise.Id)).OrderBy(a => a.Nom).ToList();

    public IEnumerable<ArmeDto> GetArmes(IEnumerable<int> ids) => ids.Select(GetArme);

    private ArmeDto GetArme(int id) => _cacheArme[id];

    #region Regroupements pour table

    // Au contact
    public ArmeAttributDto GroupeBouclier => GetAttributDArme(74);
    private ArmeAttributDto GroupeArmesDHast => GetAttributDArme(57);
    private ArmeAttributDto GroupeADeuxMains => GetAttributDArme(52);
    private ArmeAttributDto GroupeExotique => GetAttributDArme(100);

            
    public Dictionary<string, List<ArmeDto>> ArmesDeContactPourTable
    {
        get
        {
            if (_armesDeContactPourTable != null)
                return _armesDeContactPourTable;
            
            _armesDeContactPourTable = new Dictionary<string, List<ArmeDto>>
            {
                { "Ordinaires", AllArmes.Where(a => a.EstUneArmeDeCaC
                                                    && a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdMeleeOrdinaires)
                                                    && !a.Groupes.Contains(GroupeBouclier))
                    .ToList() },
                { "Armes de poing", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeBagarre)).ToList() },
                { "Armes lourdes", AllArmes.Where(a => a.Groupes.Contains(GroupeADeuxMains)
                                                       && !a.Groupes.Contains(GroupeArmesDHast)
                                                       && a.CompetencesDeMaitrise.All(s => s.Id != AptitudesService.IdMeleeFleaux))
                    .ToList() },
                { "Armes d'hast", AllArmes.Where(a => a.Groupes.Contains(GroupeArmesDHast)).ToList() },
                { "Fléaux", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeFleaux)).ToList() },
                { "Escrime", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeEscrime)).ToList() },
                { "Parade", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeParade)).ToList() },
                { "Paralysantes", AllArmes.Where(a => a.Groupes.Any(s => s.Id == AptitudesService.IdMeleeParalysantes)).ToList() },
                { "Cavalerie", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeCavalerie)).ToList() },
                { "Exotiques", AllArmes.Where(a => a.EstUneArmeDeCaC && a.Groupes.Contains(GroupeExotique)).ToList() }
            };
            return _armesDeContactPourTable;
        }
    }
    
    // A distance
    public ArmeAttributDto GroupePoudreNoire => GetAttributDArme(61);
    
    public Dictionary<string, List<ArmeDto>> ArmesADistancePourTable
    {
        get
        {
            if (_armesADistancePourTable == null)
                _armesADistancePourTable = new()
                {
                    { "Arbalètes", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirArbaletes)).ToList() },
                    { "Arcs", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirArcs)).ToList() },
                    { "Lance-pierres", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirLancePierres)).ToList() },
                    { "Poudre noire, armes mécaniques et explosifs", AllArmes.Where(a => 
                                    a.Groupes.Contains(GroupePoudreNoire) ||
                                    a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirArmesAFeu) ||
                                    a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirExplosifs)
                                ).ToList() },
                    { "Armes de jet", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirArmesDeJet)).ToList() },
                    { "Exotiques", AllArmes.Where(a => a.EstUneArmeDeTir && a.Groupes.Contains(GroupeExotique)).ToList() }
                };
            return _armesADistancePourTable;
        }
    }
    
    public ArmeAttributDto AttributDamaging => GetAttributDArme(81);
    public ArmeAttributDto AttributPercutant => GetAttributDArme(15);
    public ArmeAttributDto AttributLent => GetAttributDArme(14);
    public ArmeAttributDto AttributProtection => GetAttributDArme(89);
    public ArmeAttributDto AttributFracassant => GetAttributDArme(82);
    
    #endregion
}
