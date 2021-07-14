using System;
using System.Collections.Generic;
using BlazorWjdr.Models;
using System.Linq;

namespace BlazorWjdr.Services
{
    public class TableDesCarrieresInitialesService
    {
        private readonly RacesService _racesService;
        private readonly CarrieresService _carrieresService;
        
        private Dictionary<int, List<LigneDeCarriereInitialeDto>>? _allLignes;
        
        public TableDesCarrieresInitialesService(RacesService racesService, CarrieresService carrieresService)
        {
            _racesService = racesService;
            _carrieresService = carrieresService;
        }

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
                    Carriere = _carrieresService.GetCarriere(l.fk_carriereid),
                    Facteur = l.facteur,
                    Race = _racesService.GetRace(l.fk_raceid)
                })
                .ToList();

            _allLignes = lignesEnVrac
                .Select(l => l.Race.Id)
                .Distinct()
                .ToDictionary(k => k, _ => new List<LigneDeCarriereInitialeDto>());

            foreach (var raceId in _allLignes.Keys)
            {
                _allLignes[raceId].AddRange(lignesEnVrac
                    .Where(l => l.Race.Id == raceId)
                    .OrderBy(l => l.Carriere.Nom));
            }
        }

        public CarriereDto GetRandomStartingCareer(int raceId)
        {
            Dictionary<int, CarriereDto> dico = new();
            var key = 0;
            var plage = 0;
            foreach (var carriere in AllLignes[raceId].OrderBy(c => c.Carriere.Id))
            {
                plage += carriere.Facteur;
                for (var i = 1; i <= carriere.Facteur; i++)
                {
                    key += 1;
                    dico[key] = carriere.Carriere;
                }
            }
            
            var dice = new Random().Next(1, plage + 1);
            return dico[dice];
        }
    }
}