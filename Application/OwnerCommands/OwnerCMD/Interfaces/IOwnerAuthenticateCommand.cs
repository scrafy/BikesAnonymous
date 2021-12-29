using System.Threading.Tasks;

namespace OwnerCMD.Interfaces
{
    public interface IOwnerAuthenticateCommand
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}
