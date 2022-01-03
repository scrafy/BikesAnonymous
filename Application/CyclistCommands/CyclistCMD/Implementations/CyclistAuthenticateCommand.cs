using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using System;
using System.Threading.Tasks;

namespace CyclistCMD.Implementations
{
    public class CyclistAuthenticateCommand : ICyclistAuthenticateCommand
    {
        #region private properties

        private ICyclistRepository<Cyclist> _cyclistRepository;
        
        #endregion

        #region constructor

        public CyclistAuthenticateCommand(IRepositoryProvider repositoryProvider)
        {
            _cyclistRepository = repositoryProvider.GetCyclistRepository();           
        }

        #endregion


        #region public methods

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var cyclist = await _cyclistRepository.GetCyclistByUsernameAndPasswordAsync(username, password) ?? throw new Exception("Cyclist not found");
            //TO-DO token generation
            return "";

        }

        #endregion
    }
}
