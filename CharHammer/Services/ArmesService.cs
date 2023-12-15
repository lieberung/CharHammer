namespace CharHammer.Services;

using Models;
using System.Collections.Generic;
using System.Linq;

public class ArmesService(
    IReadOnlyDictionary<int, ArmeAttributDto> dataArmesAttributs,
    IReadOnlyDictionary<int, ArmeDto> dataArmes,
    IReadOnlyDictionary<int, ArmureDto> dataArmures,
    IReadOnlyDictionary<int, EquipementDto> dataEquipements)
{
    private IReadOnlyDictionary<string, IEnumerable<ArmeDto>>? _armesDeContactPourTable;
    private IReadOnlyDictionary<string, IEnumerable<ArmeDto>>? _armesADistancePourTable;

    public IEnumerable<ArmeAttributDto> AllGroupesDArmes => dataArmesAttributs.Values.Where(a => a.Type == "groupe").OrderBy(g => g.Nom);

    public IEnumerable<ArmeDto> AllArmes { get; } = dataArmes.Values.ToArray();
    public IEnumerable<ArmureDto> AllArmures { get; } = dataArmures.Values.ToArray();
    public IEnumerable<EquipementDto> AllEquipements { get; } = dataEquipements.Values.ToArray();
    
    private ArmeAttributDto GetAttributDArme(int id) => dataArmesAttributs[id];

    public IEnumerable<ArmeDto> GetArmesDeMaitrise(AptitudeDto maitrise) =>
        AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == maitrise.Id)).OrderBy(a => a.Nom);

    //public IEnumerable<ArmeDto> GetArmes(IEnumerable<int> ids) => ids.Select(GetArme);

    //private ArmeDto GetArme(int id) => dataArmes[id];

    #region Regroupements pour table

    // Au contact
    public ArmeAttributDto GroupeBouclier => GetAttributDArme(74);
    private ArmeAttributDto GroupeArmesDHast => GetAttributDArme(57);
    private ArmeAttributDto GroupeADeuxMains => GetAttributDArme(52);
    private ArmeAttributDto GroupeExotique => GetAttributDArme(100);

            
    public IReadOnlyDictionary<string, IEnumerable<ArmeDto>> ArmesDeContactPourTable
    {
        get
        {
            _armesDeContactPourTable ??= new Dictionary<string, IEnumerable<ArmeDto>>
            {
                { "Ordinaires", AllArmes.Where(a => a.EstUneArmeDeCaC
                                                 && a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdMeleeOrdinaires)
                                                 && !a.Groupes.Contains(GroupeBouclier))
                },
                { "Armes de poing", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeBagarre)) },
                { "Armes lourdes", AllArmes.Where(a => a.Groupes.Contains(GroupeADeuxMains)
                                                       && !a.Groupes.Contains(GroupeArmesDHast)
                                                       && a.CompetencesDeMaitrise.All(s => s.Id != AptitudesService.IdMeleeFleaux))
                     },
                { "Armes d'hast", AllArmes.Where(a => a.Groupes.Contains(GroupeArmesDHast)) },
                { "Fléaux", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeFleaux)) },
                { "Escrime", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeEscrime)) },
                { "Parade", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeParade)) },
                { "Paralysantes", AllArmes.Where(a => a.Groupes.Any(s => s.Id == AptitudesService.IdMeleeParalysantes)) },
                { "Cavalerie", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(s => s.Id == AptitudesService.IdMeleeCavalerie)) },
                { "Exotiques", AllArmes.Where(a => a.EstUneArmeDeCaC && a.Groupes.Contains(GroupeExotique)) }
            };
            return _armesDeContactPourTable;
        }
    }
    
    // A distance
    public ArmeAttributDto GroupePoudreNoire => GetAttributDArme(61);
    
    public IReadOnlyDictionary<string, IEnumerable<ArmeDto>> ArmesADistancePourTable
    {
        get
        {
            _armesADistancePourTable ??= new Dictionary<string, IEnumerable<ArmeDto>>
                {
                    { "Arbalètes", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirArbaletes)) },
                    { "Arcs", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirArcs)) },
                    { "Lance-pierres", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirLancePierres)) },
                    { "Poudre noire, armes mécaniques et explosifs", AllArmes.Where(a => 
                                    a.Groupes.Contains(GroupePoudreNoire) ||
                                    a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirArmesAFeu) ||
                                    a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirExplosifs)
                                ) },
                    { "Armes de jet", AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == AptitudesService.IdTirArmesDeJet)) },
                    { "Exotiques", AllArmes.Where(a => a.EstUneArmeDeTir && a.Groupes.Contains(GroupeExotique)) }
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
