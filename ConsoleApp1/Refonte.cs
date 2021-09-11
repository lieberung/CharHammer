using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlazorWjdr.DataSource.JsonDto;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public static class Refonte
    {
        public static void RefonteJson(
            List<JsonCompetence> skills, IEnumerable<JsonTalent> talents, IEnumerable<JsonTrait> traits,
            IEnumerable<JsonCarriere> carrieres, IEnumerable<JsonRegle> regles,
            IEnumerable<JsonBestiole> bestioles, IEnumerable<JsonPersonnage> personnages, IEnumerable<JsonPj> pjs)
        {
            var aptitudes = new List<JsonAptitude>();
            aptitudes.AddRange(skills
                .Select(c => new JsonAptitude
                {
                    id = 1000 + c.id,
                    id_old = c.id,
                    parent = c.parent.HasValue ? 1000 + c.parent.Value : null,
                    carac = c.carac,
                    categ = "skill",
                    categ_spe = c.de_base ? "bas" : "adv",
                    description = "",
                    resume = c.resume,
                    resume_v2 = c.resume_v2,
                    ignorer = c.ignorer,
                    nom = c.nom,
                    nom_en = c.nom_en,
                    spe = c.spe,
                    talents = c.talents?.Select(t => t + 2000).ToArray(),
                    traits = c.traits?.Select(t => t + 3000).ToArray()
                }));

            aptitudes.AddRange(talents.Select(t => new JsonAptitude
            {
                id = 2000 + t.id,
                id_old = t.id,
                parent = t.parent_id.HasValue ? 2000 + t.parent_id.Value : null,
                categ = "talent",
                categ_spe = t.trait ? "trait" : "",
                description = t.description,
                description_v2 = t.description_v2,
                ignorer = t.ignorer,
                max = t.max,
                nom = t.nom,
                nom_en = t.nom_en,
                spe = t.spe,
                resume = t.resume,
                resume_v2 = t.resume_v2,
                tests = t.tests,
                skills = Array.Empty<int>() // t.comp.Select(c => 1000 + c.Id).ToArray()
            }));

            aptitudes.AddRange(traits.Select(t => new JsonAptitude
            {
                id = 3000 + t.id,
                id_old = t.id,
                parent = null,
                categ = "trait",
                categ_spe = t.type,
                contagieux = t.contagieux,
                description = t.description??"",
                description_v2 = t.description_v2,
                guerison = t.guerison,
                ignorer = false,
                incompatibles = (t.incompatible??Array.Empty<int>()).Select(id => 3000 + id).ToList(),
                nom = t.nom,
                nom_en = t.nom_en,
                severite = t.severite,
                spe = t.spe,
            }));

            foreach (var apt in aptitudes)
            {
                var list = new List<int>();
                list.AddRange(apt.skills ?? Array.Empty<int>());
                list.AddRange(apt.talents ?? Array.Empty<int>());
                list.AddRange(apt.traits ?? Array.Empty<int>());
                apt.aptitudes = list.ToList();
            }
            foreach (var apt in aptitudes)
            {
                foreach (var apt_liee_id in apt.aptitudes)
                {
                    var apt_liee = aptitudes.First(a => a.id == apt_liee_id);
                    if (!apt_liee.aptitudes.Contains(apt.id))
                        apt_liee.aptitudes.Add(apt.id);
                }
                foreach (var apt_inc_id in apt.incompatibles ?? new List<int>())
                {
                    var apt_inc = aptitudes.First(a => a.id == apt_inc_id);
                    if (!apt_inc.incompatibles.Contains(apt.id))
                        apt_inc.incompatibles.Add(apt.id);
                }
            }

            foreach (var carr in carrieres)
            {
                carr.aptitudes = GetAptitudes(carr.competences ?? Array.Empty<int>(),
                    carr.talents ?? Array.Empty<int>(),
                    carr.traits ?? Array.Empty<int>());
                carr.aptitudes_choix = GetAptitudesChoix(carr.competenceschoix ?? Array.Empty<int[]>(),
                    carr.talentschoix ?? Array.Empty<int[]>());
            }

            foreach (var regle in regles)
            {
                regle.aptitudes = GetAptitudes(
                    regle.competences ?? Array.Empty<int>(),
                    regle.talents ?? Array.Empty<int>(),
                    regle.traits ?? Array.Empty<int>());
                regle.aptitudes_choix = GetAptitudesChoix(regle.choixcompetences ?? Array.Empty<int[]>(),
                    regle.choixtalents ?? Array.Empty<int[]>());
            }
            
            var creatures = bestioles.Select(b => new JsonCreature
            {
                id = b.id,
                age = b.age,
                competences = b.competences,
                talents = b.talents,
                traits = b.traits,
                aptitudes = GetAptitudes(b.competences ?? Array.Empty<int>(), b.talents ?? Array.Empty<int>(), b.traits ?? Array.Empty<int>()).ToList(),
                description = b.comment,
                membrede = b.membrede,
                nom = b.nom,
                origines = b.origines,
                poids = b.poids,
                profil_actuel = b.profil_actuel,
                psycho = b.psycho,
                race = b.race,
                sexe = b.sexe,
                taille = b.taille,
                user = b.user,
                histoire = b.histoire
            });
            foreach (var b in personnages)
            {
                var bestiole = creatures.First(c => c.id == b.id);
                bestiole.cheveux = b.cheveux;
                bestiole.fk_carrieremereid = b.fk_carrieremereid;
                bestiole.fk_carrierepereid = b.fk_carrierepereid;
                bestiole.fk_cheminprofess = b.fk_cheminprofess;
                bestiole.fk_signeastralid = b.fk_signeastralid;
                bestiole.freres_et_soeurs = b.freres_et_soeurs;
                bestiole.main_directrice = b.main_directrice;
                bestiole.mort = b.mort;
                bestiole.yeux = b.yeux;
            }
            foreach (var b in pjs)
            {
                var bestiole = creatures.First(c => c.id == b.id);
                bestiole.date_creation = b.date_creation;
                bestiole.fk_profilinitialid = b.fk_profilinitialid;
                bestiole.nom_joueur = b.nom_joueur;
                bestiole.xp_actuel = b.xp_actuel;
                bestiole.xp_total = b.xp_total;
            }

            var jsonAptitudes = JsonConvert.SerializeObject(new RootAptitude { items = aptitudes.ToList() });
            var jsonCarrieres = JsonConvert.SerializeObject(new RootCarriere { items = carrieres.ToList() });
            var jsonRegles = JsonConvert.SerializeObject(new RootRegle { items = regles.ToList() });
            var jsonCreatures = JsonConvert.SerializeObject(new RootCreature { items = creatures.ToList() });
            
            File.WriteAllText(@"C:\Users\Public\fix-aptitudes.json", jsonAptitudes);
            File.WriteAllText(@"C:\Users\Public\fix-carrieres.json", jsonCarrieres);
            File.WriteAllText(@"C:\Users\Public\fix-regles.json", jsonRegles);
            File.WriteAllText(@"C:\Users\Public\fix-creatures.json", jsonCreatures);
        }
        
        private static int[] GetAptitudes(int[] skills, int[] talents, int[] traits)
        {
            var result = new List<int>();
            result.AddRange(skills.Select(id => 1000 + id));
            result.AddRange(talents.Select(id => 2000 + id));
            result.AddRange(traits.Select(id => 3000 + id));
            return result.ToArray();
        }

        private static int[][] GetAptitudesChoix(IEnumerable<int[]> skills, IEnumerable<int[]> talents)
        {
            var result = new List<int[]>();
            result.AddRange(skills.Select(ids => ids.Select(id => 1000 + id).ToArray()).ToList());
            result.AddRange(talents.Select(ids => ids.Select(id => 2000 + id).ToArray()).ToList());
            //result.AddRange(traits.Select(ids => ids.Select(id => 3000 + id).ToArray()).ToList());
            return result.ToArray();
        }
    }
}