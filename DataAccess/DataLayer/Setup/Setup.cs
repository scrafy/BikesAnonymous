using DataLayer.Implementations;
using DataLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer.Setup
{
    public static class Setup
    {

        #region public static methods

        public static void RegisterDataLayerRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IRepositoryProvider, RepositoryProvider>();
        }

        #endregion

    }
}
