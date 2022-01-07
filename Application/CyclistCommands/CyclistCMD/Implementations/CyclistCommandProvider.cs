using DataLayer.Interfaces;
using Microsoft.Extensions.Options;
using OwnerCMD.Interfaces;
using TraversalServices.Interfaces;
using TraversalServices.Models;

namespace CyclistCMD.Implementations
{
    public class CyclistCommandProvider : ICyclistCommandProvider
    {
        #region private properties

        private ICyclistAuthenticateCommand _cyclistAuthenticateCommand;
        private IPrintLicenseCommand _printLicenseCommand;
        private IRepositoryProvider _repositoryProvider;
        private ITraversalServicesProvider _traversalServices;
        private IOptions<TokenSettings> _tokenSettings;

        #endregion

        #region constructor

        public CyclistCommandProvider(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServices, IOptions<TokenSettings> tokenSettings)
        {
            _printLicenseCommand = new PrintLicenseCommand(repositoryProvider, traversalServices);
            _cyclistAuthenticateCommand = new CyclistAuthenticateCommand(repositoryProvider, traversalServices, tokenSettings);
            _repositoryProvider = repositoryProvider;
            _traversalServices = traversalServices;
            _tokenSettings = tokenSettings;

        }

        #endregion

        #region public methods

        public ICyclistAuthenticateCommand GetCyclistAuthenticateCommand(bool singleton = false)
        {
            if (!singleton)
                return new CyclistAuthenticateCommand(_repositoryProvider, _traversalServices, _tokenSettings);

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