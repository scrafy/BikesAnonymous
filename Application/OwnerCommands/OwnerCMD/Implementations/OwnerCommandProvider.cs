using DataLayer.Interfaces;
using Microsoft.Extensions.Options;
using OwnerCMD.Interfaces;
using TraversalServices.Interfaces;
using TraversalServices.Models;

namespace OwnerCMD.Implementations
{
    public class OwnerCommandProvider : IOwnerCommandProvider
    {
        #region private properties

        private ICyclistRegisteredLastNightCommand _cyclistRegisteredLastNightCommand;
        private ILoadCSVFileCommand _loadCSVFileCommand;
        private IOwnerAuthenticateCommand _iOwnerAuthenticateCommand;
        private IOwnerCreateAccountCommand _ownerCreateAccountCommand;
        private IRepositoryProvider _repositoryProvider;
        private ITraversalServicesProvider _traversalServices;
        private IOptions<TokenSettings> _tokenSettings;

        #endregion

        #region constructor

        public OwnerCommandProvider(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServices, IOptions<TokenSettings> tokenSettings)
        {
            _cyclistRegisteredLastNightCommand = new CyclistRegisteredLastNightCommand(repositoryProvider, traversalServices);
            _loadCSVFileCommand = new LoadCSVFileCommand(repositoryProvider, traversalServices);
            _iOwnerAuthenticateCommand = new OwnerAuthenticateCommand(repositoryProvider, traversalServices, tokenSettings);
            _ownerCreateAccountCommand = new OwnerCreateAccountCommand(repositoryProvider);
            _repositoryProvider = repositoryProvider;
            _traversalServices = traversalServices;
            _tokenSettings = tokenSettings;

        }

        #endregion

        #region public methods

        public ICyclistRegisteredLastNightCommand GetCyclistRegisteredLastNightCommand(bool singleton = false)
        {
            if (!singleton)
                return new CyclistRegisteredLastNightCommand(_repositoryProvider, _traversalServices);

            return _cyclistRegisteredLastNightCommand;
        }

        public ILoadCSVFileCommand GetLoadCSVFileCommand(bool singleton = false)
        {
            if (!singleton)
                return new LoadCSVFileCommand(_repositoryProvider, _traversalServices);

            return _loadCSVFileCommand;
        }

        public IOwnerAuthenticateCommand GetOwnerAuthenticateCommand(bool singleton = false)
        {
            if (!singleton)
                return new OwnerAuthenticateCommand(_repositoryProvider, _traversalServices, _tokenSettings);

            return _iOwnerAuthenticateCommand;
        }

        public IOwnerCreateAccountCommand OwnerCreateAccountCommand(bool singleton = false)
        {
            if (!singleton)
                return new OwnerCreateAccountCommand(_repositoryProvider);

            return _ownerCreateAccountCommand;
        }

        #endregion
    }
}
