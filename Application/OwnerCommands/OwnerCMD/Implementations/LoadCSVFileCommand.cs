using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using System.Threading.Tasks;
using TraversalServices.Interfaces;
using System.Linq;

namespace OwnerCMD.Implementations
{
    public class LoadCSVFileCommand : ILoadCSVFileCommand
    {
        #region private properties

        private ICyclistRepository<Cyclist> _cyclistRepository;
        private ICSVFileParser _csvFIleParser;

        #endregion

        #region constructor

        public LoadCSVFileCommand(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServicesProvider)
        {
            _cyclistRepository = repositoryProvider.GetCyclistRepository();
            _csvFIleParser = traversalServicesProvider.GetCyclistUsersParserService();
        }

        #endregion


        #region public methods

        public async Task LoadCSVDataAsync(byte[] file)
        {
            var cyclists = (await _csvFIleParser.GetCyclistUsersAsync(file)).ToList();
            await _cyclistRepository.SaveAllAsync(cyclists);
            return;
        }

        #endregion
    }
}
