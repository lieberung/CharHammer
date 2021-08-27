using System;
using System.Diagnostics;

namespace BlazorWjdr.Services
{
    using BlazorWjdr.DataSource.JsonDto;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TraitsService
    {
        private readonly List<JsonTrait> _dataTraits;
        private Dictionary<int, TraitDto>? _cacheTrait;

        public TraitsService(List<JsonTrait> dataTraits)
        {
            _dataTraits = dataTraits;
        }

        private List<TraitDto> AllTraits
        {
            get
            {
                if (_cacheTrait == null)
                    Initialize();
                Debug.Assert(_cacheTrait != null, nameof(_cacheTrait) + " != null");
                return _cacheTrait.Values.OrderBy(t => t.Groupe).ThenBy(t => t.Nom).ThenBy(t => t.Spe).ToList();
            }
        }

        public TraitDto GetTrait(int id)
        {
            if (_cacheTrait == null)
                Initialize();
            Debug.Assert(_cacheTrait != null, nameof(_cacheTrait) + " != null");
            return _cacheTrait[id];
        }

        private void Initialize()
        {
            _cacheTrait = _dataTraits
                .Select(c => new TraitDto
                {
                    Id = c.id,
                    Nom = c.nom,
                    Spe = c.spe ?? "",
                    Groupe = c.type,
                    Description = c.description,
                    Contagieux = c.contagieux,
                    Guerison = c.guerison,
                    Severite = c.severite,
                    Incompatible = c.incompatible ?? Array.Empty<int>()
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var trait in _cacheTrait.Values)
            {
                trait.TraitsIncompatibles = trait.Incompatible.Select(id => _cacheTrait[id]).ToList();
            }
        }

        public List<TraitDto> SignesDistinctifs => AllTraits.Where(t => t.Groupe == "trait").OrderBy(t => t.NomComplet).ToList();
        public List<TraitDto> Folies => AllTraits.Where(t => t.Groupe == "folie").ToList();
        public List<TraitDto> Maladies => AllTraits.Where(t => t.Groupe == "maladie").ToList();
        public List<TraitDto> Mutations => AllTraits.Where(t => t.Groupe == "mutation").ToList();
        public List<TraitDto> Nevroses => AllTraits.Where(t => t.Groupe == "nevrose").ToList();
        public List<TraitDto> Addictions => AllTraits.Where(t => t.Groupe == "addiction").ToList();
        public List<TraitDto> Alergies => AllTraits.Where(t => t.Groupe == "allergie").ToList();
        public List<TraitDto> Phobies => AllTraits.Where(t => t.Groupe == "phobie").ToList();
        public List<TraitDto> Conditions => AllTraits.Where(t => t.Groupe == "condition").OrderBy(t => t.NomComplet).ToList();

        public TraitDto ConditionSurpris => GetTrait(460);
        public TraitDto ConditionDemoralise => GetTrait(453);
        public TraitDto ConditionATerre => GetTrait(458);
        public TraitDto ConditionEtourdi => GetTrait(459);
        public TraitDto ConditionInconscient => GetTrait(461);
        
        public TraitDto TraitPsychoHaine => GetTrait(217);
        public TraitDto TraitPsychoAnimosite => GetTrait(215);
        
        public List<TraitDto> TroublesMineurs()
        {
            var list = new List<TraitDto>();
            list.AddRange(Alergies.Where(t => t.Severite == 1));
            list.AddRange(Nevroses.Where(t => t.Severite == 1));
            list.AddRange(Phobies.Where(t => t.Severite == 1));
            return list.OrderBy(t => t.Groupe).ThenBy(t => t.Nom).ToList();
        }

        public TraitDto TirerUnSigneAleatoire(List<TraitDto> traitsDejaObtenus)
        {
            TraitDto? ta = null;
            while (ta == null
                   || traitsDejaObtenus.Contains(ta)
                   || ta.TraitsIncompatibles.Intersect(traitsDejaObtenus).Any()
                   || traitsDejaObtenus.Any(to => to.TraitsIncompatibles.Contains(ta))
            ) {
                var sd = SignesDistinctifs;
                var i = new Random().Next(0, sd.Count);
                ta = sd[i];
            }
            return ta;            
        }
    }
}
