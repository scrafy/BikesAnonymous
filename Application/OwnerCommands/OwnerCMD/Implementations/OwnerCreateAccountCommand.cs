using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using System.Threading.Tasks;

namespace OwnerCMD.Implementations
{
    public class OwnerCreateAccountCommand : IOwnerCreateAccountCommand
    {
        #region private properties

        private IOwnerRepository<Owner> _ownerRepository;

        #endregion

        #region constructor

        public OwnerCreateAccountCommand(IOwnerRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;            
        }

        #endregion

        #region public methods

        public async Task CreateAccountAsync(Owner owner)
        {
            await _ownerRepository.SaveAsync(owner);
            
        }

        #endregion
    }
}
