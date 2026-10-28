namespace CharHammer.Services.Startup;

public static class Configuration
{
    /// <summary>
    /// Déclare les services. Les données ne sont pas encore chargées : <see cref="App"/> les charge après le démarrage
    /// en affichant un écran d'attente, puis remplit le <see cref="Catalogue"/> d'où sont lus les services de données.
    /// </summary>
    public static void ConfigureServices(this IServiceCollection services, string baseAddress)
    {
        services.AddSingleton(_ => new DataLoader(new HttpClient { BaseAddress = new Uri(baseAddress) }));
        services.AddSingleton<Catalogue>();

        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Aptitudes);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Lieux);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Dieux);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().References);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Tables);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Chronologie);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Armes);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Carrieres);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Races);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().TablesDesCarrieresInitiales);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Bestioles);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Regles);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Sortileges);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Campagnes);
        services.AddSingleton(sp => sp.GetRequiredService<Catalogue>().Exiger().Scenarios);

        services.AddSingleton<AppState>();
    }
}
