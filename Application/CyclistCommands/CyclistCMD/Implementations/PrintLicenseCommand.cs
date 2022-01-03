using Core.Entities;
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

        private ICyclistRepository<Cyclist> _cyclistRepository;
        private IPDFGeneratorService _pdfGeneratorService;

        #endregion

        #region constructor

        public PrintLicenseCommand(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServicesProvider)
        {
            _cyclistRepository = repositoryProvider.GetCyclistRepository();
            _pdfGeneratorService = traversalServicesProvider.GetPDFGeneratorService();
        }

        #endregion


        #region public methods

        public async Task<byte[]> PrintLicenseAsync(Guid cyclistId)
        {
            var cyclist = await _cyclistRepository.GetAsync(cyclistId) ?? throw new Exception("Cyclist not found");
            return await _pdfGeneratorService.ObjectToPDFAsync(cyclist);


        }

        #endregion
    }
}
