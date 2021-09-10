using BlazorWjdr.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using BlazorWjdr.DataSource.JsonDto;
using BlazorWjdr.Models;
using System.Text.Json;


namespace BlazorWjdr
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var data = new ADataClassToRuleThemAllService(new HttpClient
                { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            await data.InitializeDataAsync();
            Debug.WriteLine("await data.InitializeDataAsync();");
            
            var dataTraits = InitializeTraits(data.Traits!.items);
            var dataTalents = InitializeTalents(data.Talents!.items);
            var dataCompetences = InitializeCompetences(data.Competences!.items, dataTalents, dataTraits);
            var dataProfils = InitializeProfils(data.Profils!.items);
            var dataReferences = InitializeReferences(data.References!.items);
            var dataCarrieres = InitializeCarrieres(data.Carrieres!.items, dataProfils, dataCompetences, dataTalents, dataTraits, dataReferences);
            var dataChrono = InitializeChronologie(data.Chrono!.items, dataReferences);
            var dataLieuxTypes = InitializeLieuxTypes(data.LieuxTypes!.items);
            var dataLieux = InitializeLieux(data.Lieux!.items, dataLieuxTypes);
            var dataDieux = InitializeDieux(data.Dieux!.items);
            var dataTables = InitializeTables(data.Tables!.items);
            var dataArmesAttributs = InitializeArmesAttributs(data.ArmesAttributs!.items);
            var dataArmes = InitializeArmes(data.Armes!.items, dataArmesAttributs, dataCompetences);
            var dataRaces = InitializeRaces(data.Races!.items, dataLieux);
            var dataTablesCarrInit = InitializeTablesCarrieresInitiales(data.CarrieresInitiales!.items, dataRaces, dataCarrieres);
            var dataBestioles = InitializeBestioles(data.Bestioles!.items, data.Pjs!.items, data.Personnages!.items, dataRaces, dataProfils, dataCompetences, dataTalents, dataTraits, dataLieux, dataCarrieres);
            var dataRegles = InitializeRegles(data.Regles!.items, dataTables, dataBestioles, dataCompetences, dataTalents, dataTraits, dataLieux, dataCarrieres);

            //throw new Exception("dfdddddddddddddd");
            string json = RefonteJson(dataCompetences.Values.ToArray(), dataTalents.Values.ToArray(),dataTraits.Values.ToArray(),data.Carrieres!.items, dataBestioles.Values.ToArray(),data.Regles!.items);
            
            builder.Services.AddSingleton(_ => new CompTalentsEtTraitsService(dataCompetences, dataTalents, dataTraits));
            builder.Services.AddSingleton(_ => new LieuxService(dataLieuxTypes, dataLieux));
            builder.Services.AddSingleton(_ => new DieuxService(dataDieux));
            builder.Services.AddSingleton(_ => new ReferencesService(dataReferences));
            builder.Services.AddSingleton(_ => new ProfilsService(dataProfils));
            builder.Services.AddSingleton(_ => new TablesService(dataTables));
            builder.Services.AddSingleton(_ => new ChronologieService(dataChrono));
            builder.Services.AddSingleton(_ => new ArmesService(dataArmesAttributs, dataArmes));
            builder.Services.AddSingleton(_ => new CarrieresService(dataCarrieres));
            builder.Services.AddSingleton(_ => new RacesService(dataRaces));
            builder.Services.AddSingleton(_ => new TableDesCarrieresInitialesService(dataTablesCarrInit));
            builder.Services.AddSingleton(_ => new BestiolesService(dataBestioles, json));
            builder.Services.AddSingleton(_ => new ReglesService(dataRegles));

            builder.RootComponents.Add<App>("#app");

            await builder.Build().RunAsync();
        }

        private static string RefonteJson(IEnumerable<CompetenceDto> skills, IEnumerable<TalentDto> talents,
            IEnumerable<TraitDto> traits, IEnumerable<JsonCarriere> carrieres,
            IEnumerable<BestioleDto> bestioles, IEnumerable<JsonRegle> regles)
        {
            var aptitudes = new List<JsonAptitude>();
            aptitudes.AddRange(skills
                .Select(c => new JsonAptitude
                {
                    id = 1000 + c.Id,
                    id_old = c.Id,
                    parent = c.Parent?.Id,
                    carac = c.CaracteristiqueAssociee,
                    categ = "skill",
                    categ_spe = c.EstUneCompetenceDeBase ? "bas" : "adv",
                    description = c.Resume,
                    ignorer = c.Ignore,
                    nom = c.Nom,
                    spe = c.Specialisation,
                    talents = c.TalentsLies.Select(t => t.Id + 2000).ToArray(),
                    traits = c.TraitsLies.Select(t => t.Id + 3000).ToArray()
                }));

            aptitudes.AddRange(talents.Select(t => new JsonAptitude
            {
                id = 2000 + t.Id,
                id_old = t.Id,
                parent = t.TalentParentId,
                categ = "talent",
                categ_spe = t.Trait ? "trait" : "",
                description = t.Description,
                ignorer = t.Ignore,
                max = t.Max,
                nom = t.Nom,
                spe = t.Specialisation,
                resume = t.Resume,
                tests = t.Tests,
                skills = t.CompetencesLiees.Select(c => 1000 + c.Id).ToArray()
            }));

            aptitudes.AddRange(traits.Select(t => new JsonAptitude
            {
                id = 3000 + t.Id,
                id_old = t.Id,
                categ = "trait",
                categ_spe = t.Groupe,
                contagieux = t.Contagieux,
                description = t.Description,
                guerison = t.Guerison,
                ignorer = false,
                incompatibles = t.Incompatible.Select(id => 3000 + id).ToArray(),
                nom = t.Nom,
                severite = t.Severite,
                spe = t.Spe,
            }));

            foreach (var apt in aptitudes)
            {
                var list = new List<int>();
                list.AddRange(apt.skills ?? Array.Empty<int>());
                list.AddRange(apt.talents ?? Array.Empty<int>());
                list.AddRange(apt.traits ?? Array.Empty<int>());
                apt.aptitudes = list.ToArray();
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
                id = b.Id,
                age = b.Age,
                cheveux = b.Cheveux,
                competences = GetCompetencesAcquises(b.CompetencesAcquises),
                talents = b.Talents.Select(t => t.Id).ToArray(),
                traits = b.Traits.Select(t => t.Id).ToArray(),
                date_creation = b.DateDeCreation,
                description = b.Commentaire,
                fk_carrieremereid = b.CarriereDeLaMere?.Id,
                fk_carrierepereid = b.CarriereDuPere?.Id,
                fk_cheminprofess = b.CheminementPro.Select(c => c.Id).ToArray(),
                fk_profilinitialid = b.ProfilInitial?.Id,
                fk_signeastralid = b.SigneAstralId,
                freres_et_soeurs = b.FreresEtSoeurs,
                histoire = b.Histoire,
                main_directrice = b.MainDirectrice,
                membrede = b.MembreDe,
                mort = b.Mort,
                nom = b.Nom,
                nom_joueur = b.Joueur,
                origines = b.Origines.Select(o => o.Id).ToArray(),
                poids = b.Poids,
                profil_actuel = b.ProfilActuel.Id,
                psycho = b.Psychologie,
                race = b.Race.Id,
                sexe = b.Sexe,
                taille = b.Taille,
                user = b.Userid,
                xp_actuel = b.XpActuel,
                xp_total = b.XpTotal,
                yeux = b.Yeux
            });

            string jsonAptitudes = JsonSerializer.Serialize(new RootAptitude { items = aptitudes.ToList() });
            string jsonCarrieres = JsonSerializer.Serialize(new RootCarriere { items = carrieres.ToList() });
            string jsonRegles = JsonSerializer.Serialize(new RootRegle { items = regles.ToList() });
            string jsonCreatures = JsonSerializer.Serialize(new RootCreature { items = creatures.ToList() });
            
            
/*            File.WriteAllText(@"C:\Users\lbernard\Desktop\aptitudes-fix.json", jsonAptitudes);
            File.WriteAllText(@"C:\Users\Public\carrieres-fix.json", jsonCarrieres);
            File.WriteAllText("C:\\Users\\lbernard\\Desktop\\regles-fix.json", jsonRegles);
            File.WriteAllText("C:\\Users\\lbernard\\Desktop\\creatures.json", jsonCreatures);

            File.WriteAllText("/json-data/fix-aptitudes.json", jsonAptitudes);
            File.WriteAllText("/json-data/fix-carrieres.json", jsonCarrieres);
            File.WriteAllText("/json-data/fix-regles-fix.json", jsonRegles);
            File.WriteAllText("/json-data/fix-creatures.json", jsonCreatures);

            //File.WriteAllText("C:/Users/lbernard/Desktop/fix-aptitudes.json", jsonAptitudes);
            File.WriteAllText("./wwwroot/json-data/fix-aptitudes.json", jsonAptitudes);
            File.WriteAllText("json-data/fix-aptitudes.json", jsonAptitudes);
            File.WriteAllText("json-data/fix-carrieres.json", jsonCarrieres);
            File.WriteAllText("json-data/fix-regles-fix.json", jsonRegles);
            File.WriteAllText("json-data/fix-creatures.json", jsonCreatures);
*/
            return jsonAptitudes + "\n" + jsonCarrieres + "\n" + jsonCreatures + "\n" + jsonRegles;
        }

        private static int[]? GetCompetencesAcquises(CompetenceAcquise[] argCompetencesAcquises)
        {
            if (!argCompetencesAcquises.Any())
                return null;
            List<int> result = new ();
            foreach (var ca in argCompetencesAcquises)
            {
                result.Add(ca.Competence.Id);
                if (ca.Detail.Contains("10"))
                    result.Add(ca.Competence.Id);
                if (ca.Detail.Contains("20"))
                {
                    result.Add(ca.Competence.Id);
                    result.Add(ca.Competence.Id);
                }
            }
            return result.ToArray();
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

        #region Initialize

        private static Dictionary<int, RegleDto> InitializeRegles(
            IEnumerable<JsonRegle> items,
            IReadOnlyDictionary<int, TableDto> tables,
            IReadOnlyDictionary<int, BestioleDto> bestioles,
            IReadOnlyDictionary<int, CompetenceDto> competences,
            IReadOnlyDictionary<int, TalentDto> talents,
            IReadOnlyDictionary<int, TraitDto> traits,
            IReadOnlyDictionary<int, LieuDto> lieux,
            IReadOnlyDictionary<int, CarriereDto> carrieres)
        {
            var cacheRegle = items
                .Select(r => new RegleDto
                {
                    Id = r.id,
                    Html = r.html,
                    Titre = r.titre,
                    ReglesId = r.regles ?? Array.Empty<int>(),
                    Carrieres = (r.carrieres ?? Array.Empty<int>()).Select(id => carrieres[id]).ToArray(),
                    Competences = (r.competences ?? Array.Empty<int>()).Select(id => competences[id]).ToArray(),
                    ChoixCompetences = r.choixcompetences != null
                        ? r.choixcompetences.Select(choix => choix.Select(id => competences[id]).ToArray()).ToList()
                        : new List<CompetenceDto[]>(),
                    Talents = (r.talents ?? Array.Empty<int>()).Select(id => talents[id]).ToArray(),
                    Traits = (r.traits ?? Array.Empty<int>()).Select(id => traits[id]).ToArray(),
                    ChoixTalents = r.choixtalents != null
                        ? r.choixtalents.Select(choix => choix.Select(id => talents[id]).ToArray()).ToList()
                        : new List<TalentDto[]>(),
                    Bestioles = (r.bestioles ?? Array.Empty<int>()).Select(id => bestioles[id]).ToArray(),
                    Tables = (r.tables ?? Array.Empty<int>()).Select(id => tables[id]).ToArray(),
                    Lieux = (r.lieux ?? Array.Empty<int>()).Select(id => lieux[id]).ToArray(),
                    Regle = r.regle
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var regle in cacheRegle.Values)
            {
                regle.SousRegles = regle.ReglesId.Select(id => cacheRegle[id]).ToArray();
            }

            return cacheRegle;
        }

        private static Dictionary<int, BestioleDto> InitializeBestioles(
            IEnumerable<JsonBestiole> items,
            IEnumerable<JsonPj> pjs,
            IEnumerable<JsonPersonnage> personnages,
            IReadOnlyDictionary<int, RaceDto> races,
            IReadOnlyDictionary<int, ProfilDto> profils,
            IReadOnlyDictionary<int, CompetenceDto> competences,
            IReadOnlyDictionary<int, TalentDto> talents,
            IReadOnlyDictionary<int, TraitDto> traits,
            IReadOnlyDictionary<int, LieuDto> lieux,
            IReadOnlyDictionary<int, CarriereDto> carrieres)
        {
            var cachePersonnage = personnages.ToDictionary(k => k.id);
            var cachePj = pjs.ToDictionary(k => k.id);

            return items
                .Select(c => new BestioleDto
                {
                    Id = c.id,
                    EstUnPersonnage = cachePersonnage.ContainsKey(c.id),
                    EstUnPersonnageJoueur = cachePj.ContainsKey(c.id),
                    Userid = c.user,
                    MembreDe = c.membrede,
                    Age = c.age,
                    Commentaire = c.comment,
                    Histoire = c.histoire,
                    Nom = c.nom,
                    Poids = c.poids,
                    Psychologie = c.psycho,
                    Race = races[c.race],
                    Sexe = c.sexe,
                    Taille = c.taille,
                    ProfilActuel = profils[c.profil_actuel],
                    CompetencesAcquises = CompetenceAcquise.GetList(
                        (c.competences ?? Array.Empty<int>()).Select(id => competences[id]).ToArray()
                    ),
                    Talents = (c.talents ?? Array.Empty<int>()).Select(id => talents[id]).ToArray(),
                    Origines = (c.origines ?? Array.Empty<int>()).Select(id => lieux[id]).ToArray(),
                    Traits = (c.traits ?? Array.Empty<int>()).Select(id => traits[id]).ToArray(),
                    // Personnage
                    SigneAstralId = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].fk_signeastralid : null,
                    Cheveux = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].cheveux : "",
                    Yeux = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].yeux : "",
                    FreresEtSoeurs = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].freres_et_soeurs : "",
                    MainDirectrice = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].main_directrice : 0,
                    Mort = cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].mort,
                    CarriereDuPere =
                        cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].fk_carrierepereid.HasValue
                            ? carrieres[cachePersonnage[c.id].fk_carrierepereid!.Value]
                            : null,
                    CarriereDeLaMere =
                        cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].fk_carrieremereid.HasValue
                            ? carrieres[cachePersonnage[c.id].fk_carrieremereid!.Value]
                            : null,
                    // PJ
                    Joueur = cachePj.ContainsKey(c.id) ? cachePj[c.id].nom_joueur : "",
                    CheminementPro = cachePersonnage.ContainsKey(c.id)
                        ? cachePersonnage[c.id].fk_cheminprofess != null ? cachePersonnage[c.id].fk_cheminprofess!
                            .Select(id => carrieres[id]).ToArray()
                        : Array.Empty<CarriereDto>()
                        : Array.Empty<CarriereDto>(),

                    ProfilInitial = cachePj.ContainsKey(c.id) ? profils[cachePj[c.id].fk_profilinitialid] : null,
                    XpActuel = cachePj.ContainsKey(c.id) ? cachePj[c.id].xp_actuel : 0,
                    XpTotal = cachePj.ContainsKey(c.id) ? cachePj[c.id].xp_total : 0,
                })
                .ToDictionary(k => k.Id);
        }

        private static Dictionary<int, List<LigneDeCarriereInitialeDto>> InitializeTablesCarrieresInitiales(
            IEnumerable<JsonTableCarriereInitiale> items,
            IReadOnlyDictionary<int, RaceDto> races,
            IReadOnlyDictionary<int, CarriereDto> carrieres)
        {
            var lignesEnVrac = items
                .Select(l => new LigneDeCarriereInitialeDto
                {
                    Carriere = carrieres[l.carriere],
                    Facteur = l.facteur,
                    Race = races[l.race]
                })
                .ToList();

            var allLignes = lignesEnVrac
                .Select(l => l.Race.Id)
                .Distinct()
                .ToDictionary(k => k, _ => new List<LigneDeCarriereInitialeDto>());

            foreach (var raceId in allLignes.Keys)
            {
                allLignes[raceId].AddRange(lignesEnVrac
                    .Where(l => l.Race.Id == raceId)
                    .OrderBy(l => l.Carriere.Groupe)
                    .ThenBy(l => l.Carriere.Nom)
                );
            }

            return allLignes;
        }

        private static Dictionary<int, RaceDto> InitializeRaces(IEnumerable<JsonRace> items,
            Dictionary<int, LieuDto> lieux)
        {
            var cache = items
                .Select(r => new RaceDto
                {
                    Id = r.id,
                    Description = r.description,
                    Lieux = (r.lieux_ids ?? Array.Empty<int>()).Select(id => lieux[id]).ToArray(),
                    GroupOnly = r.group_only,
                    NomFeminin = r.nom_feminin,
                    NomMasculin = r.nom_masculin,
                    ParentId = r.parent_id,
                    PourPj = r.pj
                })
                .ToDictionary(k => k.Id);

            foreach (var race in cache.Values.Where(d => d.ParentId.HasValue))
            {
                race.Parent = cache[race.ParentId!.Value];
            }

            foreach (var lieu in cache.Values)
            {
                lieu.SousElements.AddRange(cache.Values
                    .Where(c => c.Parent == lieu)
                    .OrderBy(c => c.NomMasculin));
            }

            return cache;
        }

        private static Dictionary<int, ArmeAttributDto> InitializeArmesAttributs(IEnumerable<JsonArmeAttribut> items)
        {
            return items
                .Select(t => new ArmeAttributDto
                {
                    Id = t.id,
                    Type = t.type,
                    Nom = t.nom,
                    Description = t.description
                }).ToDictionary(k => k.Id);
        }

        private static Dictionary<int, ArmeDto> InitializeArmes(
            IEnumerable<JsonArme> items,
            IReadOnlyDictionary<int, ArmeAttributDto> cacheAttributs,
            IReadOnlyDictionary<int, CompetenceDto> cacheCompetences)
        {
            return items
                .Select(l => new ArmeDto
                {
                    Id = l.id,
                    Nom = l.nom,
                    Description = l.description ?? "",
                    Attributs = l.attributs.Select(id => cacheAttributs[id]).ToList(),
                    Degats = l.degats,
                    Disponibilite = l.dispo,
                    Encombrement = l.enc,
                    Groupes = l.groupes.Select(id => cacheAttributs[id]).ToList(),
                    Allonge = l.allonge ?? "",
                    Portee = l.portee ?? "",
                    Prix = l.prix,
                    Rechargement = l.rechargement ?? "",
                    CompetencesDeMaitrise = l.competences.Select(id => cacheCompetences[id]).ToList()
                })
                .ToDictionary(k => k.Id);
        }

        private static Dictionary<int, TableDto> InitializeTables(IEnumerable<JsonTable> items)
        {
            return items
                .Select(c => new TableDto
                {
                    Id = c.id,
                    Titre = c.titre,
                    Description = c.description,
                    StylesHeader = c.styles_th ?? Array.Empty<string>(),
                    StylesRows = c.styles_td ?? Array.Empty<string>(),
                    Lignes = c.lignes
                })
                .ToDictionary(k => k.Id, v => v);
        }

        private static Dictionary<int, DieuDto> InitializeDieux(IEnumerable<JsonDieu> items)
        {
            var cache = items
                .Select(c => new DieuDto
                {
                    Id = c.id,
                    Nom = c.nom,
                    Commentaire = c.comment,
                    Domaines = c.domaines,
                    Fideles = c.fideles,
                    Histoire = c.histoire,
                    Symboles = c.symboles,
                    SymbolesImages = c.symboles_image
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var dieu in cache.Values.Where(d => d.PatronId.HasValue))
            {
                dieu.Patron = cache[dieu.PatronId!.Value];
            }

            return cache;
        }

        private static Dictionary<int, LieuTypeDto> InitializeLieuxTypes(IEnumerable<JsonLieuType> items)
        {
            var result = items
                .Select(t => new LieuTypeDto
                {
                    Id = t.id,
                    Nom = t.libelle,
                    ParentId = t.parentid
                }).ToDictionary(k => k.Id, v => v);

            foreach (var lieuType in result.Values.Where(t => t.ParentId.HasValue))
            {
                lieuType.Parent = result[lieuType.ParentId!.Value];
            }

            return result;
        }

        private static Dictionary<int, LieuDto> InitializeLieux(IEnumerable<JsonLieu> items,
            IReadOnlyDictionary<int, LieuTypeDto> cacheTypesDeLieu)
        {
            var result = items
                .Select(l => new LieuDto
                {
                    Id = l.id,
                    Nom = l.nom,
                    Description = l.description ?? "",
                    ParentId = l.fk_parentid,
                    TypeDeLieu = cacheTypesDeLieu[l.fk_typeid]
                })
                .ToDictionary(k => k.Id);

            var parents = new List<int>();
            foreach (var lieu in result.Values.Where(t => t.ParentId.HasValue))
            {
                var parentId = lieu.ParentId!.Value;
                if (!parents.Contains(parentId))
                    parents.Add(parentId);
                lieu.Parent = result[parentId];
                lieu.Parent.SousElements.Add(lieu);
            }

            //foreach (var lieu in result.Values.Where(l => l.ParentId == null))
            foreach (var lieuId in (parents))
            {
                result[lieuId].SousElements = result[lieuId].SousElements.OrderBy(c => c.Nom).ToList();
                /*
                .AddRange(result.Values
                .Where(c=>c.Parent == lieu)
                .OrderBy(c => c.Nom));
                */
            }

            return result;
        }

        private static Dictionary<int, ReferenceDto> InitializeReferences(IEnumerable<JsonReference> items)
        {
            return items
                .Select(c => new ReferenceDto
                {
                    Id = c.id,
                    AnneeDePublication = c.publishyear ?? 6666,
                    Code = c.code,
                    Titre = c.titre,
                    Version = c.version
                })
                .ToDictionary(k => k.Id, v => v);
        }

        private static Dictionary<int, TalentDto> InitializeTalents(IEnumerable<JsonTalent> talents)
        {
            var result = talents
                .Select(t => new TalentDto
                {
                    Id = t.id,
                    Nom = $"{t.nom}{(!string.IsNullOrWhiteSpace(t.spe) ? $" : {t.spe}" : "")}",
                    Description = t.description,
                    Ignore = t.ignorer,
                    Resume = t.resume,
                    Specialisation = t.spe ?? "",
                    TalentParentId = t.parent_id,
                    Trait = t.trait,
                    Max = t.max ?? "",
                    Tests = t.tests ?? ""
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var talent in result.Values.Where(t => t.TalentParentId.HasValue))
            {
                talent.Parent = result[talent.TalentParentId!.Value];
            }

            foreach (var talent in result.Values.Where(t => t.Parent != null).Select(t => t.Parent).Distinct())
                talent!.SousElements.AddRange(result.Values
                    .Where(c => c.Parent == talent)
                    .OrderBy(c => c.Nom));

            foreach (var talent in result.Values)
            {
                talent.NomPourRecherche = GenericService.ConvertirCaracteres(talent.Nom);
                talent.MotsClefDeRecherche = GenericService.MotsClefsDeRecherche(talent.NomPourRecherche);
            }

            return result;
        }

        private static Dictionary<int, CompetenceDto> InitializeCompetences(
            IEnumerable<JsonCompetence> competences,
            Dictionary<int, TalentDto> cacheTalents,
            IReadOnlyDictionary<int, TraitDto> cacheTraits)
        {
            var result = competences
                .Select(c => new CompetenceDto
                {
                    Id = c.id,
                    Ignore = c.ignorer,
                    Nom = $"{c.nom}{(!string.IsNullOrWhiteSpace(c.spe) ? $" : {c.spe}" : "")}",
                    Resume = c.resume,
                    Specialisation = c.spe ?? "",
                    CaracteristiqueAssociee = c.carac,
                    EstUneCompetenceDeBase = c.de_base,
                    CompetenceMereId = c.parent,
                    TalentsLies = (c.talents ?? Array.Empty<int>())
                        .Select(id => cacheTalents[id])
                        .OrderBy(t => t.Nom).ThenBy(t => t.Specialisation)
                        .ToList(),
                    TraitsLies = (c.traits ?? Array.Empty<int>())
                        .Select(id => cacheTraits[id])
                        .OrderBy(t => t.Nom).ThenBy(t => t.Spe)
                        .ToList()
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var competence in result.Values.Where(c => c.CompetenceMereId.HasValue))
            {
                competence.Parent = result[competence.CompetenceMereId!.Value];
            }

            foreach (var competence in result.Values)
            {
                competence.NomPourRecherche = GenericService.ConvertirCaracteres(competence.Nom);
                competence.MotsClefDeRecherche = GenericService.MotsClefsDeRecherche(competence.NomPourRecherche);
                competence.SetResume();
            }

            foreach (var competence in result.Values.Where(t => t.Parent != null).Select(t => t.Parent).Distinct())
            {
                competence!.SousElements.AddRange(result.Values
                    .Where(c => c.Parent == competence)
                    .OrderBy(c => c.Nom));
            }

            foreach (var talent in cacheTalents.Values)
            {
                talent.CompetencesLiees = result.Values
                    .Where(c => c.TalentsLies.Contains(talent) && c.Ignore == false)
                    .ToList();
            }

            return result;
        }

        private static Dictionary<int, TraitDto> InitializeTraits(IEnumerable<JsonTrait> traits)
        {
            var cacheTrait = traits
                .Select(c => new TraitDto
                {
                    Id = c.id,
                    Nom = c.nom,
                    Spe = c.spe ?? "",
                    Groupe = c.type,
                    Description = c.description ?? "",
                    Contagieux = c.contagieux,
                    Guerison = c.guerison ?? "",
                    Severite = c.severite,
                    Incompatible = c.incompatible ?? Array.Empty<int>()
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var trait in cacheTrait.Values)
            {
                trait.TraitsIncompatibles = trait.Incompatible.Select(id => cacheTrait[id]).ToList();
            }

            return cacheTrait;
        }

        private static Dictionary<int, ProfilDto> InitializeProfils(IEnumerable<JsonProfil> profils)
        {
            return profils
                .Select(c => new ProfilDto
                {
                    Id = c.id,
                    A = c.a,
                    Ag = c.ag,
                    //B = c.b,
                    Cc = c.cc,
                    Ct = c.ct,
                    Dex = c.dex,
                    E = c.e,
                    F = c.f,
                    Fm = c.fm,
                    I = c.i,
                    Int = c.intel,
                    //M = c.m,
                    //Mag = c.mag,
                    //Pd = c.pd,
                    //Pf = c.pf,
                    Soc = c.soc
                })
                .ToDictionary(k => k.Id, v => v);
        }

        private static IEnumerable<CompetenceDto> GetCompetences(IEnumerable<int>? ids,
            IReadOnlyDictionary<int, CompetenceDto> cache)
            => (ids ?? Array.Empty<int>()).Select(id => cache[id]).OrderBy(c => c.Nom).ThenBy(c => c.Specialisation);

        private static IEnumerable<TalentDto> GetTalents(IEnumerable<int>? ids,
            IReadOnlyDictionary<int, TalentDto> cache)
            => (ids ?? Array.Empty<int>()).Select(id => cache[id]).OrderBy(t => t.Nom).ThenBy(t => t.Specialisation);

        private static IEnumerable<TraitDto> GetTraits(IEnumerable<int>? ids, IReadOnlyDictionary<int, TraitDto> cache)
            => (ids ?? Array.Empty<int>()).Select(id => cache[id]).OrderBy(t => t.Nom).ThenBy(t => t.Spe);

        private static IEnumerable<CarriereDto> GetCarrieres(IEnumerable<int>? ids,
            IReadOnlyDictionary<int, CarriereDto> cache)
            => (ids ?? Array.Empty<int>()).Select(id => cache[id]).OrderBy(c => c.Nom);

        private static Dictionary<int, CarriereDto> InitializeCarrieres(
            IEnumerable<JsonCarriere> carrieres,
            IReadOnlyDictionary<int, ProfilDto> cacheProfils,
            IReadOnlyDictionary<int, CompetenceDto> cacheCompetences,
            IReadOnlyDictionary<int, TalentDto> cacheTalents,
            IReadOnlyDictionary<int, TraitDto> cacheTraits,
            IReadOnlyDictionary<int, ReferenceDto> cacheReferences)
        {
            var cacheCarrieres = carrieres
                .Select(c => new CarriereDto
                {
                    Id = c.id,
                    Groupe = c.groupe ?? "",
                    Nom = c.nom,
                    MotsClefDeRecherche =
                        GenericService.MotsClefsDeRecherche(GenericService.ConvertirCaracteres(c.nom)),
                    Description = c.description,
                    Ambiance = c.ambiance ?? Array.Empty<string>(),
                    CarriereMereId = c.parent,
                    DebouchesIds = c.debouch ?? Array.Empty<int>(),
                    AvancementsIds = c.avancements ?? Array.Empty<int>(),
                    Dotations = c.dotations,
                    EstUneCarriereAvancee = c.avancee,
                    Image = $"images/careers/{c.id}.png",
                    PlanDeCarriere = cacheProfils[c.plan],
                    Restriction = c.restriction ?? "",
                    Source = c.source_page ?? "",
                    SourceLivre = c.source_livre == null ? null : cacheReferences[c.source_livre.Value],
                    Competences = GetCompetences(c.competences, cacheCompetences).ToList(),
                    Talents = GetTalents(c.talents, cacheTalents).ToList(),
                    ChoixCompetences = c.competenceschoix != null
                        ? c.competenceschoix.Select(choix => GetCompetences(choix, cacheCompetences).ToArray()).ToList()
                        : new List<CompetenceDto[]>(),
                    ChoixTalents = c.talentschoix != null
                        ? c.talentschoix.Select(choix => GetTalents(choix, cacheTalents).ToArray()).ToList()
                        : new List<TalentDto[]>(),
                    Traits = GetTraits(c.traits, cacheTraits).ToList(),
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var carriere in cacheCarrieres.Values.Where(c => c.CarriereMereId.HasValue))
            {
                carriere.Parent = cacheCarrieres[carriere.CarriereMereId!.Value];
            }

            foreach (var carriere in cacheCarrieres.Values.Where(c => c.DebouchesIds.Any()))
                carriere.Debouches = GetCarrieres(carriere.DebouchesIds, cacheCarrieres).ToList();
            foreach (var carriere in cacheCarrieres.Values.Where(c => c.AvancementsIds.Any()))
                carriere.Avancements = GetCarrieres(carriere.AvancementsIds, cacheCarrieres).ToList();

            foreach (var carriere in cacheCarrieres.Values.Where(c => c.Parent != null).Select(c => c.Parent)
                .Distinct())
            {
                carriere!.SousElements.AddRange(cacheCarrieres.Values
                    .Where(c => c.Parent == carriere)
                    .OrderBy(c => c.Nom));
            }

            foreach (var carriere in cacheCarrieres.Values)
            {
                carriere.Filieres = cacheCarrieres.Values
                    .Where(c => c.Debouches.Contains(carriere))
                    .OrderBy(c => c.Nom)
                    .ToList();
                carriere.Origines = cacheCarrieres.Values
                    .Where(c => c.Avancements.Contains(carriere))
                    .OrderBy(c => c.Nom)
                    .ToList();
            }

            return cacheCarrieres;
        }

        private static IEnumerable<ChronologieDto> InitializeChronologie(IEnumerable<JsonChrono> chrono,
            IReadOnlyDictionary<int, ReferenceDto> cache)
        {
            return chrono
                .Select(c => new ChronologieDto(
                    c.debut,
                    c.fin,
                    c.resume,
                    c.titre ?? "",
                    c.comment ?? "",
                    c.sources.Select(id => cache[id]).ToArray()
                ))
                .OrderBy(c => c.Debut).ThenBy(c => c.Fin)
                .ToArray();
        }

        #endregion
    }
}