using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using TraversalServices.Interfaces;
using System.Linq;
using TraversalServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace OwnerCMD.Implementations
{
    public class LoadCSVFileCommand : ILoadCSVFileCommand
    {
        #region private properties

        private IRepositoryProvider _repositoryProvider;
        private ITraversalServicesProvider _traversalServicesProvider;

        #endregion

        #region constructor

        public LoadCSVFileCommand(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServicesProvider)
        {
            _repositoryProvider = repositoryProvider;
            _traversalServicesProvider = traversalServicesProvider;
        }

        #endregion


        #region public methods

        public async Task LoadCSVDataAsync(byte[] file)
        {
            var cyclists = (await _traversalServicesProvider.GetCyclistUsersParserService().GetCyclistUsersAsync(file)).ToList();
            if (cyclists.Count() == 0)
                return;
            await _repositoryProvider.GetCyclistRepository().SaveAllAsync(cyclists);
            SendLicensesToCylists(cyclists);           
        }

        private async void SendLicensesToCylists( IEnumerable<Cyclist> cyclists )
        {
            var emailService = _traversalServicesProvider.GetEmailService();
            cyclists.ToList().ForEach(async c =>
            {
                var attachment = new Dictionary<string, byte[]>();
                attachment.Add("license.pdf", await _traversalServicesProvider.GetPDFGeneratorService().ObjectToPDFAsync(c));
                var emailSettings = new EmailSettings()
                {

                    To = new string[] { c.Email },
                    Body = "Has been registered in bikesanonymous, then you can download your license which is attached to this email.\n\nKind regards",
                    Subject = "Has been registered in bikesanonymous",
                    Attachments = attachment
                };
                emailService.SendEmailAsync(emailSettings);
            });
        }

        #endregion
    }
}
