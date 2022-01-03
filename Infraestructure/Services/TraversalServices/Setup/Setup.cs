using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TraversalServices.Implementations;
using TraversalServices.Interfaces;
using TraversalServices.Models;


namespace TraversalServices.Setup
{
    public static class Setup
    {
        #region public static methods

        public static void RegisterInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITraversalServicesProvider, TraversalServicesProvider>();
        }

        public static void LoadInfraestructureConfigurationSections(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<EmailSection>(options => configuration.GetSection("EmailSection").Bind(options));
            services.Configure<TokenSettings>(options => configuration.GetSection("TokenSettings").Bind(options));
        }

        #endregion
    }
}
