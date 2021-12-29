using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TraversalServices.Setup;
using DataLayer.Setup;
using OwnerCMD.Setup;

namespace BikesAnonymous
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            LoadConfigurationSections(services);
            RegisterProviderServices(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterProviderServices(IServiceCollection services)
        {
            services.AddControllers();
            services.RegisterInfraestructureServices(Configuration);
            services.RegisterDataLayerRepositories();
            services.RegisterOwnerCommandsProvider();
        }

        private void LoadConfigurationSections(IServiceCollection services)
        {
            services.LoadInfraestructureConfigurationSections(Configuration);
        }
    }
}
