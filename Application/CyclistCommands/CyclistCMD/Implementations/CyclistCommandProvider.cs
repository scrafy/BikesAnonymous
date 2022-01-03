using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using TraversalServices.Interfaces;

namespace CyclistCMD.Implementations
{
    public class CyclistCommandProvider : ICyclistCommandProvider
    {
        #region private properties

        private ICyclistAuthenticateCommand _cyclistAuthenticateCommand;
        private IPrintLicenseCommand _printLicenseCommand;
        private IRepositoryProvider _repositoryProvider;
        private ITraversalServicesProvider _traversalServices;

        #endregion

        #region constructor

        public CyclistCommandProvider(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServices)
        {
            _printLicenseCommand = new PrintLicenseCommand(repositoryProvider, traversalServices);
            _cyclistAuthenticateCommand = new CyclistAuthenticateCommand(repositoryProvider);
            _repositoryProvider = repositoryProvider;
            _traversalServices = traversalServices;

        }

        #endregion

        #region public methods

        public ICyclistAuthenticateCommand GetCyclistAuthenticateCommand(bool singleton = false)
        {
            if (!singleton)
                return new CyclistAuthenticateCommand(_repositoryProvider);

            return _cyclistAuthenticateCommand;
        }

        public IPrintLicenseCommand GetPrintLicenseCommand(bool singleton = false)
        {
            if (!singleton)
                return new PrintLicenseCommand(_repositoryProvider, _traversalServices);

            return _printLicenseCommand;

        }

        #endregion
    }
}