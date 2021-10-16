using System;

namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class BestiolesService
    {
        private readonly Dictionary<int, BestioleDto> _cacheBestiole;

        public BestiolesService(Dictionary<int, BestioleDto> data)
        {
            _cacheBestiole = data;
        }
        
        public IEnumerable<BestioleDto> AllBestioles => _cacheBestiole.Values.ToList();
        public BestioleDto GetBestiole(int id) => _cacheBestiole[id];

        public static AptitudeDto? GetGabarit(BestioleDto bestiole)
        {
            return bestiole.AptitudesAcquises.Select(aa => aa.Aptitude)
                .FirstOrDefault(a => a.Parent?.Id == AptitudesService.AptitudeGroupeGabarit);
        }

        public static int CalculBlessures(AptitudeDto gabarit, ProfilDto profil, bool durACuir)
        {
            var blessures = gabarit.Id switch
            {
                AptitudesService.TraitGabaritMinusculeId => 1,
                AptitudesService.TraitGabaritToutPetitId => profil.Be,
                AptitudesService.TraitGabaritPetitId => (2 * profil.Be) + profil.Bfm,
                AptitudesService.TraitGabaritMoyenId => profil.Bf + (2 * profil.Be) + profil.Bfm,
                AptitudesService.TraitGabaritLargeId => 2 * (profil.Bf + (2 * profil.Be) + profil.Bfm),
                AptitudesService.TraitGabaritEnormeId => 4 * (profil.Bf + (2 * profil.Be) + profil.Bfm),
                AptitudesService.TraitGabaritMonstrueuxId => 8 * (profil.Bf + (2 * profil.Be) + profil.Bfm),
                _ => throw new Exception("Gabarit invalide pour calcul des Blessures")
            };
            if (durACuir)
                blessures += profil.Be;
            return blessures;
        }

        public static string GetBlessuresDetailDuCalcul(AptitudeDto gabarit, ProfilDto profil, bool durACuir)
        {
            var blessures = gabarit.Id switch
            {
                AptitudesService.TraitGabaritMinusculeId => "",
                AptitudesService.TraitGabaritToutPetitId => "",
                AptitudesService.TraitGabaritPetitId => $"(2 * {profil.Be}) + {profil.Bfm}",
                AptitudesService.TraitGabaritMoyenId => $"{profil.Bf} + (2 * {profil.Be}) + {profil.Bfm}",
                AptitudesService.TraitGabaritLargeId => $"2 * ({profil.Bf} + (2 * {profil.Be}) + {profil.Bfm})",
                AptitudesService.TraitGabaritEnormeId => $"4 * ({profil.Bf} + (2 * {profil.Be}) + {profil.Bfm})",
                AptitudesService.TraitGabaritMonstrueuxId => $"8 * ({profil.Bf} + (2 * {profil.Be}) + {profil.Bfm})",
                _ => throw new Exception("Gabarit invalide pour détail du calcul des Blessures")
            };
            if (durACuir)
                blessures += $" + {profil.Be}*";
            return blessures;
        }

        public static string GetBlessuresFormuleDeCalcul(AptitudeDto gabarit, ProfilDto profil, bool durACuir)
        {
            var blessures = gabarit.Id switch
            {
                AptitudesService.TraitGabaritMinusculeId => "(gabarit minuscule)",
                AptitudesService.TraitGabaritToutPetitId => "BE",
                AptitudesService.TraitGabaritPetitId => "(2 * BE) + BFM",
                AptitudesService.TraitGabaritMoyenId => "BF + (2 * BE) + BFM",
                AptitudesService.TraitGabaritLargeId => "2 * (BF + (2 * BE) + BFM)",
                AptitudesService.TraitGabaritEnormeId => "4 * (BF + (2 * BE) + BFM)",
                AptitudesService.TraitGabaritMonstrueuxId => "8 * (BF + (2 * BE) + BFM)",
                _ => throw new Exception("Gabarit invalide pour formule de calcul des Blessures")
            };
            if (durACuir)
                blessures += $" + BE (dur à cuir)";
            return blessures;

        }
    }
}
