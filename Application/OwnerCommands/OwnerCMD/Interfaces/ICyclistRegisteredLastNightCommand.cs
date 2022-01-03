using System.Threading.Tasks;

namespace OwnerCMD.Interfaces
{
    public interface ICyclistRegisteredLastNightCommand
    {
        Task GetCylistRegisteredLastNightAsync();
    }
}
