using Core.Enums;
using Core.Exceptions;
using DataLayer.Interfaces;
using Microsoft.Extensions.Options;
using OwnerCMD.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraversalServices.Interfaces;
using TraversalServices.Models;

namespace CyclistCMD.Implementations
{
    public class CyclistAuthenticateCommand : ICyclistAuthenticateCommand
    {
        #region private properties

        private IRepositoryProvider _repositoryProvider;
        private ITraversalServicesProvider _traversalServicesProvider;
        private IOptions<TokenSettings> _tokenSettings;


        #endregion

        #region constructor

        public CyclistAuthenticateCommand(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServicesProvider, IOptions<TokenSettings> tokenSettings)
        {
            _repositoryProvider = repositoryProvider;
            _traversalServicesProvider = traversalServicesProvider;
            _tokenSettings = tokenSettings;
        }

        #endregion


        #region public methods

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var cyclist = await _repositoryProvider.GetCyclistRepository().GetCyclistByUsernameAndPasswordAsync(username, password) ?? throw new GenericException("Cyclist not found", ErrorCode.NOT_FOUND);
            var claims = new Dictionary<string, string>();
            claims.Add("role", ROLE.CYCLIST.ToString());
            claims.Add("Id", cyclist.Id.ToString());
            return _traversalServicesProvider.GetTokenGeneratorService().CreateToken(_tokenSettings.Value.Secret, claims);

        }

        #endregion
    }
}
