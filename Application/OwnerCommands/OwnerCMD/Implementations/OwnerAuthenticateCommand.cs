using Core.Enums;
using Core.Exceptions;
using DataLayer.Interfaces;
using Microsoft.Extensions.Options;
using OwnerCMD.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraversalServices.Interfaces;
using TraversalServices.Models;

namespace OwnerCMD.Implementations
{
    public class OwnerAuthenticateCommand : IOwnerAuthenticateCommand
    {
        #region private properties

        private IRepositoryProvider _repositoryProvider;
        private ITraversalServicesProvider _traversalServicesProvider;
        private IOptions<TokenSettings> _tokenSettings;


        #endregion

        #region constructor

        public OwnerAuthenticateCommand(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServicesProvider, IOptions<TokenSettings> tokenSettings)
        {
            _repositoryProvider = repositoryProvider;
            _traversalServicesProvider = traversalServicesProvider;
            _tokenSettings = tokenSettings;
        }

        #endregion


        #region public methods

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var owner = (await _repositoryProvider.GetOwnerRepository().GetOwnerByUsernameAndPasswordAsync(username, password)) ?? throw new GenericException("Owner not found", ErrorCode.NOT_FOUND);
            var claims = new Dictionary<string, string>();
            claims.Add("role", ROLE.OWNER.ToString());
            return _traversalServicesProvider.GetTokenGeneratorService().CreateToken(_tokenSettings.Value.Secret, claims);
        }

        #endregion
    }
}
