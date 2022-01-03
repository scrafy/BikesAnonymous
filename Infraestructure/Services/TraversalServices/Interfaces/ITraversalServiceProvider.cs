namespace TraversalServices.Interfaces
{
    public interface ITraversalServicesProvider
    {
        ICSVFileParser GetCyclistUsersParserService(bool singleton = false);
        IPDFGeneratorService GetPDFGeneratorService(bool singleton = false);
        IEmailService GetEmailService(bool singleton = false);
        ITokenGeneratorService GetTokenGeneratorService(bool singleton = false);
    }
}
