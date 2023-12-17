using CharHammer;
using CharHammer.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

#region Code perso
var dataLoader = new DataLoader(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var data = await dataLoader.LoadData();
builder.Services.ConfigureServices(data);
#endregion

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
