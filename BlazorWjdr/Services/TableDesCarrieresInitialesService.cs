﻿using System;
using System.Collections.Generic;
using BlazorWjdr.Models;
using System.Linq;
using BlazorWjdr.DataSource.JsonDto;

namespace BlazorWjdr.Services
{
    public class TableDesCarrieresInitialesService
    {
        private List<JsonTableCarriereInitiale> _data;
        private readonly RacesService _racesService;
        private readonly CarrieresService _carrieresService;
        
        private Dictionary<int, List<LigneDeCarriereInitialeDto>>? _allLignes;
        
        public TableDesCarrieresInitialesService(List<JsonTableCarriereInitiale> data, RacesService racesService, CarrieresService carrieresService)
        {
            _data = data;
            _racesService = racesService;
            _carrieresService = carrieresService;
        }

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
            var lignesEnVrac = _data
                .Select(l => new LigneDeCarriereInitialeDto
                {
                    Carriere = _carrieresService.GetCarriere(l.carriere),
                    Facteur = l.facteur,
                    Race = _racesService.GetRace(l.race)
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
                    .OrderBy(l => l.Carriere.Groupe)
                    .ThenBy(l => l.Carriere.Nom)
                );
            }

            //_data.Clear();
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