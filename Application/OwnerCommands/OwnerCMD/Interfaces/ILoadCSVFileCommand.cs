using System.Threading.Tasks;

namespace OwnerCMD.Interfaces
{
    public interface ILoadCSVFileCommand
    {
        Task LoadCSVDataAsync(byte[] file);
    }
}
