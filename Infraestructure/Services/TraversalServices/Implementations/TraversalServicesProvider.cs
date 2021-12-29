using Microsoft.Extensions.Options;
using TraversalServices.Interfaces;
using TraversalServices.Models;

namespace TraversalServices.Implementations
{
    public class TraversalServicesProvider : ITraversalServicesProvider
    {
        #region private properties

        private ICSVFileParser _cyclistUserParser;
        private IEmailService _mailService;
        private IPDFGeneratorService _pdfGeneratorService;
        private IOptions<EmailSection> _emailSection;

        #endregion


        #region constructor

        public TraversalServicesProvider(IOptions<EmailSection> emailSection)
        {
            _emailSection = emailSection;
            _cyclistUserParser = new CSVFileParser();
            _pdfGeneratorService = new PDFGeneratorService();
            _mailService = new EmailService(_emailSection);
        }

        #endregion


        #region public methods

        public ICSVFileParser GetCyclistUsersParserService(bool singleton = false)
         {
             if ( !singleton )
                 return new CSVFileParser();

             return _cyclistUserParser;
         }

        public IPDFGeneratorService GetPDFGeneratorService(bool singleton = false)
        {
            if (!singleton)
                return new PDFGeneratorService();

            return _pdfGeneratorService;
        }

        public IEmailService GetEmailService(bool singleton = false)
        {
            if (!singleton)
                return new EmailService(_emailSection);

            return _mailService;
        }

        #endregion
    }
}
