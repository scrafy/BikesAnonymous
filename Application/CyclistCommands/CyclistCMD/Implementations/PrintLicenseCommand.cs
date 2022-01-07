using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using System;
using System.Threading.Tasks;
using TraversalServices.Interfaces;

namespace CyclistCMD.Implementations
{
    public class PrintLicenseCommand : IPrintLicenseCommand
    {
        #region private properties

        private IRepositoryProvider _repositoryProvider;
        private ITraversalServicesProvider _traversalServicesProvider;

        #endregion

        #region constructor

        public PrintLicenseCommand(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServicesProvider)
        {
            _repositoryProvider = repositoryProvider;
            _traversalServicesProvider = traversalServicesProvider;
        }

        #endregion


        #region public methods

        public async Task<byte[]> PrintLicenseAsync(Guid cyclistId)
        {
            var cyclist = await _repositoryProvider.GetCyclistRepository().GetAsync(cyclistId) ?? throw new Exception("Cyclist not found");
            return await _traversalServicesProvider.GetPDFGeneratorService().ObjectToPDFAsync(cyclist);


        }

        #endregion
    }
}
