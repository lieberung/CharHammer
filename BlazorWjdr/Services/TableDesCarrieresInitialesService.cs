using System;
using System.Collections.Generic;
using BlazorWjdr.Models;
using System.Linq;
using BlazorWjdr.DataSource.JsonDto;

namespace BlazorWjdr.Services
{
    public class TableDesCarrieresInitialesService
    {
        private Dictionary<int, List<LigneDeCarriereInitialeDto>> _allLignes;
        
        public TableDesCarrieresInitialesService(Dictionary<int, List<LigneDeCarriereInitialeDto>> data)
        {
            _allLignes = data;
        }

        public Dictionary<int, List<LigneDeCarriereInitialeDto>> AllLignes => _allLignes;

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