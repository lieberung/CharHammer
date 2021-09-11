namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    
    public class ArmesService
    {
        private readonly Dictionary<int, ArmeAttributDto> _cacheArmeAttribut;

        private readonly Dictionary<int, ArmeDto> _cacheArme;

        private Dictionary<string, List<ArmeDto>>? _armesDeContactPourTable;
        private Dictionary<string, List<ArmeDto>>? _armesADistancePourTable;

        public ArmesService(Dictionary<int, ArmeAttributDto> dataArmesAttributs, Dictionary<int, ArmeDto> dataArmes)
        {
            _cacheArmeAttribut = dataArmesAttributs;
            _cacheArme = dataArmes;
        }

        public List<ArmeAttributDto> AllGroupesDArmes => _cacheArmeAttribut.Values.Where(a => a.Type == "groupe").OrderBy(g => g.Nom).ToList();

        public List<ArmeDto> AllArmes => _cacheArme.Values.ToList();
        
        private ArmeAttributDto GetAttributDArme(int id) => _cacheArmeAttribut[id];

        public List<ArmeDto> GetArmesDeMaitrise(AptitudeDto maitrise) =>
            AllArmes.Where(a => a.CompetencesDeMaitrise.Any(c => c.Id == maitrise.Id)).OrderBy(a => a.Nom).ToList();

        public IEnumerable<ArmeDto> GetArmes(IEnumerable<int> ids) => ids.Select(GetArme).ToArray();

        private ArmeDto GetArme(int id) => _cacheArme[id];

        #region Regroupements pour table

        // Au contact
        public ArmeAttributDto GroupeOrdinaires => GetAttributDArme(50);
        public ArmeAttributDto GroupeFléaux => GetAttributDArme(55);
        public ArmeAttributDto GroupeEscrime => GetAttributDArme(76);
        public ArmeAttributDto GroupeCavalerie => GetAttributDArme(77);
        public ArmeAttributDto GroupeParade => GetAttributDArme(56);
        public ArmeAttributDto GroupeBouclier => GetAttributDArme(74);
        public ArmeAttributDto GroupeParalysantes => GetAttributDArme(53);
        public ArmeAttributDto GroupeArmesDHast => GetAttributDArme(57);
        public ArmeAttributDto GroupeADeuxMains => GetAttributDArme(52);
        public ArmeAttributDto GroupeExotique => GetAttributDArme(100);
                
        public Dictionary<string, List<ArmeDto>> ArmesDeContactPourTable
        {
            get
            {
                if (_armesDeContactPourTable == null)
                    _armesDeContactPourTable = new Dictionary<string, List<ArmeDto>>
                    {
                        { "Ordinaires", AllArmes.Where(a => a.EstUneArmeDeCaC
                                                                && a.Groupes.Contains(GroupeOrdinaires)
                                                                && !a.Groupes.Contains(GroupeBouclier)
                                                    ).ToList() },
                        { "Armes lourdes", AllArmes.Where(a => a.Groupes.Contains(GroupeADeuxMains)
                                                                   && !a.Groupes.Contains(GroupeArmesDHast)
                                                                   && !a.Groupes.Contains(GroupeFléaux)
                                                    ).ToList() },
                        { "Armes d'hast", AllArmes.Where(a => a.Groupes.Contains(GroupeArmesDHast)).ToList() },
                        { "Fléaux", AllArmes.Where(a => a.Groupes.Contains(GroupeFléaux)).ToList() },
                        { "Escrime", AllArmes.Where(a => a.Groupes.Contains(GroupeEscrime)).ToList() },
                        { "Défense et parade", AllArmes.Where(a => a.Groupes.Contains(GroupeBouclier) || a.Groupes.Contains(GroupeParade)).ToList() },
                        { "Paralysantes", AllArmes.Where(a => a.Groupes.Contains(GroupeParalysantes)).ToList() },
                        { "Cavalerie", AllArmes.Where(a => a.Groupes.Contains(GroupeCavalerie)).ToList() },
                        { "Exotiques", AllArmes.Where(a => a.EstUneArmeDeCaC && a.Groupes.Contains(GroupeExotique)).ToList() }
                    };
                return _armesDeContactPourTable;
            }
        }
        
        // A distance
        public ArmeAttributDto GroupeArbalètes => GetAttributDArme(69);
        public ArmeAttributDto GroupeArcs => GetAttributDArme(70);
        public ArmeAttributDto GroupePoudreNoire => GetAttributDArme(61);
        public ArmeAttributDto GroupeMécaniques => GetAttributDArme(62);
        public ArmeAttributDto GroupeExplosifs => GetAttributDArme(60);
        public ArmeAttributDto GroupeArmesDeJet => GetAttributDArme(54);
        public ArmeAttributDto GroupeLancePierres => GetAttributDArme(58);
        
        public Dictionary<string, List<ArmeDto>> ArmesADistancePourTable
        {
            get
            {
                if (_armesADistancePourTable == null)
                    _armesADistancePourTable = new Dictionary<string, List<ArmeDto>>
                    {
                        { "Arbalètes", AllArmes.Where(a => a.Groupes.Contains(GroupeArbalètes)).ToList() },
                        { "Arcs", AllArmes.Where(a => a.Groupes.Contains(GroupeArcs)).ToList() },
                        { "Lance-pierres", AllArmes.Where(a => a.Groupes.Contains(GroupeLancePierres)).ToList() },
                        { "Poudre noire, armes mécaniques et explosifs", AllArmes.Where(a => 
                                        a.Groupes.Contains(GroupePoudreNoire) ||
                                        a.Groupes.Contains(GroupeMécaniques) ||
                                        a.Groupes.Contains(GroupeExplosifs)
                                    ).ToList() },
                        { "Armes de jet", AllArmes.Where(a => a.Groupes.Contains(GroupeArmesDeJet)).ToList() },
                        { "Exotiques", AllArmes.Where(a => a.EstUneArmeDeTir && a.Groupes.Contains(GroupeExotique)).ToList() }
                    };
                return _armesADistancePourTable;
            }
        }
        
        public ArmeAttributDto AttributDamaging => GetAttributDArme(81);
        public ArmeAttributDto AttributPercutant => GetAttributDArme(15);
        public ArmeAttributDto AttributLent => GetAttributDArme(14);
        
        #endregion
    }
}
