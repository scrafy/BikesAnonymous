namespace OwnerCMD.Interfaces
{
    public interface ICyclistCommandProvider
    {
        ICyclistAuthenticateCommand GetCyclistAuthenticateCommand(bool singleton = false);

        IPrintLicenseCommand GetPrintLicenseCommand(bool singleton = false);

    }
}
