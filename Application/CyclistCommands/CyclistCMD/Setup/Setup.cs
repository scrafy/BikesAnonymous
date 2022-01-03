using CyclistCMD.Implementations;
using Microsoft.Extensions.DependencyInjection;
using OwnerCMD.Interfaces;

namespace OwnerCMD.Setup
{
    public static class Setup
    {

        #region public static methods

        public static void RegisterCyclistCommandsProvider(this IServiceCollection services)
        {
            services.AddSingleton<ICyclistCommandProvider, CyclistCommandProvider>();
        }

        #endregion

    }
}
