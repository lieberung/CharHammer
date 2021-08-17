namespace BlazorWjdr
{
    using Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<DieuxService>();
            services.AddSingleton<TraitsService>();
            services.AddSingleton<RacesService>();
            services.AddSingleton<ReferencesService>();
            services.AddSingleton<LieuxService>();
            services.AddSingleton<ArmesService>();
            services.AddSingleton<ChronologieService>();
            services.AddSingleton<ProfilsService>();
            services.AddSingleton<CompetencesEtTalentsService>();
            services.AddSingleton<CarrieresService>();
            services.AddSingleton<TablesService>();
            services.AddSingleton<ReglesService>();
            services.AddSingleton<TableDesCarrieresInitialesService>();
            services.AddSingleton<BestiolesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
