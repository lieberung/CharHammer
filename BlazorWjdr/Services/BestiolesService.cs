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
        public IEnumerable<BestioleDto> AllPnjs => _cacheBestiole.Values
            .Where(b => b.EstUnPersonnage && !b.EstUnPersonnageJoueur).OrderBy(b => b.Nom).ToArray()
            .ToList();
        public IEnumerable<BestioleDto> AllPjs => _cacheBestiole.Values
            .Where(b => b.EstUnPersonnageJoueur).OrderBy(b => b.Nom).ToArray()
            .ToList();

        private IEnumerable<string> GroupesDeBestioles() => AllBestioles
            .Where(b => b.EstUnPersonnage == false)
            .SelectMany(b => b.MembreDe)
            .Distinct()
            .OrderBy(g => g); 
        private IEnumerable<BestioleDto> BestiolesMembreDuGroupe(string groupe) => AllBestioles
            .Where(b => b.EstUnPersonnage == false && b.MembreDe.Contains(groupe))
            .OrderBy(b => b.Nom)
            .ToArray();
        public IReadOnlyDictionary<string, IEnumerable<BestioleDto>> BestiolesRegroupees()
            => GroupesDeBestioles().ToDictionary(k => k, BestiolesMembreDuGroupe);

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

        public static CombattantDto[] InitiativeDeCombat(IEnumerable<BestioleDto> combattants)
        {
            return combattants
                .Select(JetDInitiativeDeCombat)
                .OrderByDescending(idc => idc.JetDInitiative)
                .ThenByDescending(idc => idc.Combattant.ProfilActuel.I)
                .ToArray();
        }

        private static CombattantDto JetDInitiativeDeCombat(BestioleDto combattant)
        {
            var initiative = (combattant.ProfilActuel.BonusDInitiative * 2) + combattant.ProfilActuel.BonusDAgilite;
            var detail = $"2 x {combattant.ProfilActuel.BonusDInitiative} (BI) + {combattant.ProfilActuel.BonusDAgilite} (BAg)";
            
            var reflexes = combattant.AptitudesAcquises.Count(a => a.Aptitude.Id == AptitudesService.TalentReflexesDeCombatId);
            if (reflexes != 0)
            {
                initiative += reflexes * 2;
                detail += $" + {reflexes * 2} (RdC)";
            }
            
            var dice = 1 + new Random().Next(0, 10);
            initiative += dice;
            detail += $" + {dice} (1d10)";

            return new CombattantDto(combattant, initiative, detail);
        }
    }
}
