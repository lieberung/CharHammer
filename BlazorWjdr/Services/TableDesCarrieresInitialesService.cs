using System.Collections.Generic;
using BlazorWjdr.Models;
using System.Linq;

namespace BlazorWjdr.Services
{
    public class TableDesCarrieresInitialesService
    {
        private readonly CarrieresService _carrieresService;
        
        private Dictionary<int, List<LigneDeCarriereInitialeDto>>? _allLignes = null;
        
        public TableDesCarrieresInitialesService(CarrieresService carrieresService)
        {
            _carrieresService = carrieresService;
        }

        public int GetSommeDesFacteurs(int raceId) => AllLignes[raceId].Sum(l => l.Facteur);
        public int GetNombreDeCarrieres(int raceId) => AllLignes[raceId].Count;

        public Dictionary<int, List<LigneDeCarriereInitialeDto>> AllLignes
        {
            get
            {
                if (_allLignes == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null reference return.
                return _allLignes;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        private void Initialize()
        {
            var lignesEnVrac = DataSource.JsonLoader
                .GetRootTableCarriereInitiale()
                .items
                .Select(l => new LigneDeCarriereInitialeDto
                {
                    Id = l.id,
                    Carriere = _carrieresService.GetCarriere(l.fk_carriereid),
                    Facteur = l.facteur,
                    RaceId = l.fk_raceid
                })
                .ToList();

            _allLignes = lignesEnVrac
                .Select(l => l.RaceId)
                .Distinct()
                .ToDictionary(k => k, v => new List<LigneDeCarriereInitialeDto>());

            foreach (var raceId in _allLignes.Keys)
            {
                _allLignes[raceId].AddRange(lignesEnVrac
                    .Where(l => l.RaceId == raceId)
                    .OrderBy(l => l.Carriere.Nom));
            }
        }
    }
}