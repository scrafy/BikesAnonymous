using System;
using System.Collections.Generic;
using System.Text;

namespace OwnerCMD.Interfaces
{
    public interface IOwnerCommandProvider
    {
        ICyclistRegisteredLastNightCommand GetCyclistRegisteredLastNightCommand(bool singleton = false);

        ILoadCSVFileCommand GetLoadCSVFileCommand(bool singleton = false);

        IOwnerAuthenticateCommand GetOwnerAuthenticateCommand(bool singleton = false);

        IOwnerCreateAccountCommand OwnerCreateAccountCommand(bool singleton = false);
    }
}
