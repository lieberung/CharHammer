using CharHammer;
using CharHammer.Services.Startup;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Les données sont chargées après le démarrage, par App (écran d'attente, puis page d'erreur en cas d'échec).
builder.Services.ConfigureServices(builder.HostEnvironment.BaseAddress);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
