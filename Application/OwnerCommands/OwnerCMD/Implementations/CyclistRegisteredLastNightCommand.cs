using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using TraversalServices.Interfaces;
using System.Linq;
using System;
using System.Text;
using System.Collections.Generic;
using TraversalServices.Models;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Enums;

namespace OwnerCMD.Implementations
{
    public class CyclistRegisteredLastNightCommand : ICyclistRegisteredLastNightCommand
    {
        #region private properties

        private IRepositoryProvider _repositoryProvider;
        private ITraversalServicesProvider _traversalServicesProvider;

        #endregion

        #region constructor

        public CyclistRegisteredLastNightCommand(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServicesProvider)
        {
            _traversalServicesProvider = traversalServicesProvider;
            _repositoryProvider = repositoryProvider;
        }

        #endregion

        #region public methods

        public async Task GetCylistRegisteredLastNightAsync()
        {
            var owner = (await _repositoryProvider.GetOwnerRepository().AllAsync())?.FirstOrDefault();

            if (owner == null)
                throw new GenericException("Any owner account has been registered", ErrorCode.NOT_FOUND);

            var start = new DateTime(DateTime.UtcNow.Ticks).Date;
            var end = new DateTime(DateTime.UtcNow.Ticks).Date.AddHours(12).AddMilliseconds(-1);
            var cyclists = (await _repositoryProvider.GetCyclistRepository().AllAsync())?.ToList() ?? new List<Cyclist>();
            cyclists = cyclists.Where(c => c.LicenseRegistrationDate >= start && c.LicenseRegistrationDate <= end).ToList();
            if(cyclists.Count > 0)
            {
                SendReport(cyclists, owner.Email);
            }

        }

        private async Task SendReport(IEnumerable<Cyclist> cyclists, string ownerEmail)
        {
            var body = new StringBuilder();

            cyclists.ToList().ForEach(c => body.AppendLine($"{c.FirstName} {c.LastName} --> {c.LicenseCode}"));
            body.AppendLine($"\n\nA total of {cyclists.Count()} cyclist/cylists have been registered last night");
            _traversalServicesProvider.GetEmailService().SendEmailAsync(new EmailSettings()
            {

                To = new string[] { ownerEmail },
                Subject = "Cyclist resgitered last night report",
                Body = body.ToString(),

            });
        }

        #endregion
    }
}
