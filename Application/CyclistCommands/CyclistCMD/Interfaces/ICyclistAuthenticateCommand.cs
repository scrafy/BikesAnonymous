using System.Threading.Tasks;

namespace OwnerCMD.Interfaces
{
    public interface ICyclistAuthenticateCommand
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}
