using System;
using System.Threading.Tasks;

namespace OwnerCMD.Interfaces
{
    public interface IPrintLicenseCommand
    {
        Task<byte[]> PrintLicenseAsync(Guid cyclistId);
    }
}
