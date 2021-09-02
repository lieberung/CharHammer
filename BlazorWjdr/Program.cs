using BlazorWjdr.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using BlazorWjdr.DataSource.JsonDto;
using BlazorWjdr.Models;

namespace BlazorWjdr
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            //builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            //builder.Services.AddSingleton<ADataClassToRuleThemAllService>();
            
            var data = new ADataClassToRuleThemAllService(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            await data.InitializeDataAsync();

            // Sans dépendances
            var dataTraits = InitializeTraits(data.Traits!.items);
            var dataTalents = InitializeTalents(data.Talents!.items);
            var dataCompetences = InitializeCompetences(data.Competences!.items, dataTalents, dataTraits);
            builder.Services.AddSingleton(s => new CompTalentsEtTraitsService(dataCompetences, dataTalents, dataTraits));
            builder.Services.AddSingleton(s => new LieuxService(data.LieuxTypes!.items, data.Lieux!.items));
            builder.Services.AddSingleton(s => new DieuxService(data.Dieux!.items));
            builder.Services.AddSingleton(s => new ReferencesService(data.References!.items));
            builder.Services.AddSingleton(s => new ProfilsService(data.Profils!.items));
            builder.Services.AddSingleton(s => new TablesService(data.Tables!.items));

            // Avec dépendances sans dépendance
            var competencesEtTalentsService = builder.Services.BuildServiceProvider().GetRequiredService<CompTalentsEtTraitsService>();
            builder.Services.AddSingleton(s => new ArmesService(data.ArmesAttributs!.items, data.Armes!.items, competencesEtTalentsService));

            var referencesService = builder.Services.BuildServiceProvider().GetRequiredService<ReferencesService>();
            builder.Services.AddSingleton(s => new ChronologieService(data.Chrono!.items, referencesService));

            // Avec dépendances de baisé
            var lieuxService = builder.Services.BuildServiceProvider().GetRequiredService<LieuxService>();
            var profilsService = builder.Services.BuildServiceProvider().GetRequiredService<ProfilsService>();
            builder.Services.AddSingleton(s => new RacesService(data.Races!.items, lieuxService, profilsService));

            builder.Services.AddSingleton(s => new CarrieresService(data.Carrieres!.items, profilsService, competencesEtTalentsService, referencesService));

            var carrieresService = builder.Services.BuildServiceProvider().GetRequiredService<CarrieresService>();
            var racesService = builder.Services.BuildServiceProvider().GetRequiredService<RacesService>();
            builder.Services.AddSingleton(s => new TableDesCarrieresInitialesService(data.CarrieresInitiales!.items, racesService, carrieresService));

            builder.Services.AddSingleton(s => new BestiolesService(data.Bestioles!.items, data.Pjs!.items, data.Personnages!.items, racesService, competencesEtTalentsService, lieuxService, profilsService, carrieresService));

            var bestiolesService = builder.Services.BuildServiceProvider().GetRequiredService<BestiolesService>();
            var tablesService = builder.Services.BuildServiceProvider().GetRequiredService<TablesService>();
            builder.Services.AddSingleton(s => new ReglesService(data.Regles!.items, carrieresService, competencesEtTalentsService, bestiolesService, tablesService, lieuxService));

            builder.RootComponents.Add<App>("#app");

            await builder.Build().RunAsync();
        }

        private static Dictionary<int, TalentDto> InitializeTalents(List<JsonTalent> talents)
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
                    .Where(c=>c.Parent == talent)
                    .OrderBy(c => c.Nom));

            foreach (var talent in result.Values)
            {
                talent.NomPourRecherche = GenericService.ConvertirCaracteres(talent.Nom);
                talent.MotsClefDeRecherche = GenericService.MotsClefsDeRecherche(talent.NomPourRecherche);
            }
            
            return result;
        }

        private static Dictionary<int, CompetenceDto> InitializeCompetences(
            List<JsonCompetence> competences, 
            Dictionary<int, TalentDto> cacheTalents,
            Dictionary<int, TraitDto> cacheTraits)
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
                    .ToList()                })
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
                    .Where(c=>c.Parent == competence)
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

        private static Dictionary<int, TraitDto> InitializeTraits(List<JsonTrait> traits)
        {
            var cacheTrait = traits
                .Select(c => new TraitDto
                {
                    Id = c.id,
                    Nom = c.nom,
                    Spe = c.spe ?? "",
                    Groupe = c.type,
                    Description = c.description,
                    Contagieux = c.contagieux,
                    Guerison = c.guerison,
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
    }
}
