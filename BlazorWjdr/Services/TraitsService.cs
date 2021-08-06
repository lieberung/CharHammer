namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TraitsService
    {
        private Dictionary<int, TraitDto>? _cacheTrait;
        private List<TraitDto>? _allTraits;

        private List<TraitDto> AllTraits
        {
            get
            {
                if (_allTraits == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null Trait return.
                return _allTraits;
#pragma warning restore CS8603 // Possible null Trait return.
            }
        }
        
        public Task<TraitDto[]> Items()
        {
            return Task.FromResult(AllTraits.ToArray());
        }

        public TraitDto GetTrait(int id)
        {
            if (_cacheTrait == null)
                Initialize();
#pragma warning disable CS8602 // DeTrait of a possibly null Trait.
            return _cacheTrait[id];
#pragma warning restore CS8602 // DeTrait of a possibly null Trait.
        }

        private void Initialize()
        {
            _cacheTrait = DataSource.JsonLoader
                .GetRootTrait()
                .items
                .Select(c => new TraitDto
                {
                    Id = c.id,
                    Nom = c.nom,
                    Spe = c.spe,
                    Groupe = c.type,
                    Description = c.description,
                    Contagieux = c.contagieux,
                    Guerison = c.guerison,
                    Severite = c.severite
                })
                .ToDictionary(k => k.Id, v => v);
            
            _allTraits = _cacheTrait.Values.OrderBy(t => t.Groupe).ThenBy(t => t.Nom).ToList();
        }

        public List<TraitDto> SignesDistinctifs => AllTraits.Where(t => t.Groupe == "trait").ToList();
        public List<TraitDto> Folies => AllTraits.Where(t => t.Groupe == "folie").ToList();
        public List<TraitDto> Maladies => AllTraits.Where(t => t.Groupe == "maladie").ToList();
        public List<TraitDto> Mutations => AllTraits.Where(t => t.Groupe == "mutation").ToList();
        public List<TraitDto> Nevroses => AllTraits.Where(t => t.Groupe == "nevrose").ToList();
        public List<TraitDto> Addictions => AllTraits.Where(t => t.Groupe == "addiction").ToList();
        public List<TraitDto> Alergies => AllTraits.Where(t => t.Groupe == "allergie").ToList();
        public List<TraitDto> Phobies => AllTraits.Where(t => t.Groupe == "phobie").ToList();

        public List<TraitDto> TroublesMineurs()
        {
            var list = new List<TraitDto>();
            list.AddRange(Alergies.Where(t => t.Severite == 1));
            list.AddRange(Nevroses.Where(t => t.Severite == 1));
            list.AddRange(Phobies.Where(t => t.Severite == 1));
            return list.OrderBy(t => t.Groupe).ThenBy(t => t.Nom).ToList();
        }
    }
}
