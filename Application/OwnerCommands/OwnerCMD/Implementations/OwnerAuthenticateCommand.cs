using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using System;
using System.Threading.Tasks;

namespace OwnerCMD.Implementations
{
    public class OwnerAuthenticateCommand : IOwnerAuthenticateCommand
    {
        #region private properties

        private IOwnerRepository<Owner> _ownerRepository;

        #endregion

        #region constructor

        public OwnerAuthenticateCommand(IOwnerRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;            
        }

        #endregion


        #region public methods

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var owner = await _ownerRepository.GetOwnerByUsernameAndPasswordAsync(username, password) ?? throw new Exception("Owner not found");
            return "";//generar token
            
        }

        #endregion
    }
}
