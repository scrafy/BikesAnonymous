namespace TraversalServices.Interfaces
{
    public interface ITraversalServicesProvider
    {
        ICyclistUserParserService GetCyclistUsersParserService(bool singleton = false);
        IPDFGeneratorService GetPDFGeneratorService(bool singleton = false);
        IEmailService GetEmailService(bool singleton = false);
    }
}
