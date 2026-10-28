using CharHammer;
using CharHammer.Services.Startup;
using CharHammer.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

#region Code perso
// Les données doivent être chargées avant de déclarer les services. Si le chargement ou l'initialisation échoue,
// on affiche une page d'erreur à la place de l'application au lieu de rester bloqué sur le cercle de chargement.
try
{
    var dataLoader = new DataLoader(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    var data = await dataLoader.LoadData();
    builder.Services.ConfigureServices(data);
    builder.RootComponents.Add<App>("#app");
}
catch (Exception exception)
{
    Console.Error.WriteLine($"Échec du chargement des données : {exception}");
    builder.RootComponents.Add(typeof(ErreurDeChargement), "#app", ParameterView.FromDictionary(new Dictionary<string, object?>
    {
        [nameof(ErreurDeChargement.Exception)] = exception
    }));
}
#endregion

builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
