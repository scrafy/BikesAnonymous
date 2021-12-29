using Microsoft.Extensions.DependencyInjection;
using OwnerCMD.Implementations;
using OwnerCMD.Interfaces;

namespace OwnerCMD.Setup
{
    public static class Setup
    {

        #region public static methods

        public static void RegisterOwnerCommandsProvider(this IServiceCollection services)
        {
            services.AddSingleton<IOwnerCommandProvider, OwnerCommandProvider>();
        }

        #endregion

    }
}
