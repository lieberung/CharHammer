using System.Collections.Generic;
using BlazorWjdr.Models;
using System.Linq;

namespace BlazorWjdr.Services
{
    public class TableDesCarrieresInitialesService
    {
        public TableDesCarrieresInitialesService(Dictionary<int, List<LigneDeCarriereInitialeDto>> data)
        {
            AllLignes = data;
        }

        public Dictionary<int, List<LigneDeCarriereInitialeDto>> AllLignes { get; }

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
            
            var dice = GenericService.RollDice(plage);
            return dico[dice];
        }
    }
}