using BlazorWjdr.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorWjdr.DataSource.JsonDto;

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
            builder.Services.AddSingleton(s => new CompetencesEtTalentsService(data.Competences!.items, data.Talents!.items));
            builder.Services.AddSingleton(s => new LieuxService(data.LieuxTypes!.items, data.Lieux!.items));
            builder.Services.AddSingleton(s => new DieuxService(data.Dieux!.items));
            builder.Services.AddSingleton(s => new TraitsService(data.Traits!.items));
            builder.Services.AddSingleton(s => new ReferencesService(data.References!.items));
            builder.Services.AddSingleton(s => new ProfilsService(data.Profils!.items));
            builder.Services.AddSingleton(s => new TablesService(data.Tables!.items));

            // Avec dépendances sans dépendance
            var competencesEtTalentsService = builder.Services.BuildServiceProvider().GetRequiredService<CompetencesEtTalentsService>();
            builder.Services.AddSingleton(s => new ArmesService(data.ArmesAttributs!.items, data.Armes!.items, competencesEtTalentsService));

            var referencesService = builder.Services.BuildServiceProvider().GetRequiredService<ReferencesService>();
            builder.Services.AddSingleton(s => new ChronologieService(data.Chrono!.items, referencesService));

            // Avec dépendances de baisé
            var lieuxService = builder.Services.BuildServiceProvider().GetRequiredService<LieuxService>();
            var profilsService = builder.Services.BuildServiceProvider().GetRequiredService<ProfilsService>();
            builder.Services.AddSingleton(s => new RacesService(data.Races!.items, lieuxService, profilsService));

            var traitsService = builder.Services.BuildServiceProvider().GetRequiredService<TraitsService>();
            builder.Services.AddSingleton(s => new CarrieresService(data.Carrieres!.items, profilsService, competencesEtTalentsService, referencesService, traitsService));

            var carrieresService = builder.Services.BuildServiceProvider().GetRequiredService<CarrieresService>();
            var racesService = builder.Services.BuildServiceProvider().GetRequiredService<RacesService>();
            builder.Services.AddSingleton(s => new TableDesCarrieresInitialesService(data.CarrieresInitiales!.items, racesService, carrieresService));

            builder.Services.AddSingleton(s => new BestiolesService(data.Bestioles!.items, data.Pjs!.items, data.Personnages!.items, racesService, competencesEtTalentsService, lieuxService, profilsService, carrieresService, traitsService));

            var bestiolesService = builder.Services.BuildServiceProvider().GetRequiredService<BestiolesService>();
            var tablesService = builder.Services.BuildServiceProvider().GetRequiredService<TablesService>();
            builder.Services.AddSingleton(s => new ReglesService(data.Regles!.items, carrieresService, competencesEtTalentsService, traitsService, bestiolesService, tablesService, lieuxService));

            builder.RootComponents.Add<App>("#app");

            await builder.Build().RunAsync();
        }
    }
}
