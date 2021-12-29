using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using TraversalServices.Interfaces;

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

        #endregion

        #region constructor

        public OwnerCommandProvider(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServices)
        {
            _cyclistRegisteredLastNightCommand = new CyclistRegisteredLastNightCommand(repositoryProvider.GetOwnerRepository(), repositoryProvider.GetCyclistRepository(), traversalServices.GetEmailService());
            _loadCSVFileCommand = new LoadCSVFileCommand(repositoryProvider.GetCyclistRepository(), traversalServices.GetCyclistUsersParserService());
            _iOwnerAuthenticateCommand = new OwnerAuthenticateCommand(repositoryProvider.GetOwnerRepository());
            _ownerCreateAccountCommand = new OwnerCreateAccountCommand(repositoryProvider.GetOwnerRepository());
            _repositoryProvider = repositoryProvider;
            _traversalServices = traversalServices;

        }

        #endregion

        #region public methods

        public ICyclistRegisteredLastNightCommand GetCyclistRegisteredLastNightCommand(bool singleton = false)
        {
            if (!singleton)
                return new CyclistRegisteredLastNightCommand(_repositoryProvider.GetOwnerRepository(), _repositoryProvider.GetCyclistRepository(), _traversalServices.GetEmailService());

            return _cyclistRegisteredLastNightCommand;
        }

        public ILoadCSVFileCommand GetLoadCSVFileCommand(bool singleton = false)
        {
            if (!singleton)
                return new LoadCSVFileCommand(_repositoryProvider.GetCyclistRepository(), _traversalServices.GetCyclistUsersParserService());

            return _loadCSVFileCommand;
        }

        public IOwnerAuthenticateCommand GetOwnerAuthenticateCommand(bool singleton = false)
        {
            if (!singleton)
                return new OwnerAuthenticateCommand(_repositoryProvider.GetOwnerRepository());

            return _iOwnerAuthenticateCommand;
        }

        public IOwnerCreateAccountCommand OwnerCreateAccountCommand(bool singleton = false)
        {
            if (!singleton)
                return new OwnerCreateAccountCommand(_repositoryProvider.GetOwnerRepository());

            return _ownerCreateAccountCommand;
        }

        #endregion
    }
}
