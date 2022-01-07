using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using System.Threading.Tasks;

namespace OwnerCMD.Implementations
{
    public class OwnerCreateAccountCommand : IOwnerCreateAccountCommand
    {
        #region private properties

        private IRepositoryProvider _repositoryProvider;

        #endregion

        #region constructor

        public OwnerCreateAccountCommand(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        #endregion

        #region public methods

        public async Task CreateAccountAsync(Owner owner)
        {
            await _repositoryProvider.GetOwnerRepository().SaveAsync(owner);
            
        }

        #endregion
    }
}
