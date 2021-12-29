using Core.Entities;
using System.Threading.Tasks;

namespace OwnerCMD.Interfaces
{
    public interface IOwnerCreateAccountCommand
    {
        Task CreateAccountAsync(Owner owner);
    }
}
