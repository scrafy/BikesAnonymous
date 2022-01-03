using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using System.Threading.Tasks;
using TraversalServices.Interfaces;
using System.Linq;
using System;
using TraversalServices.Models;
using System.Text;
using System.Collections.Generic;

namespace OwnerCMD.Implementations
{
    public class CyclistRegisteredLastNightCommand : ICyclistRegisteredLastNightCommand
    {
        #region private properties

        private ICyclistRepository<Cyclist> _cyclistRepository;
        private IOwnerRepository<Owner> _ownerRepository;
        private IEmailService _emailService;

        #endregion

        #region constructor

        public CyclistRegisteredLastNightCommand(IRepositoryProvider repositoryProvider, ITraversalServicesProvider traversalServicesProvider)
        {
            _cyclistRepository = repositoryProvider.GetCyclistRepository();
            _ownerRepository = repositoryProvider.GetOwnerRepository();
            _emailService = traversalServicesProvider.GetEmailService();
        }

        #endregion

        #region public methods

        public async Task GetCylistRegisteredLastNightAsync()
        {
            var owner = (await _ownerRepository.AllAsync())?.FirstOrDefault();

            if (owner == null)
                throw new Exception("Any owner account has been registered");

            var start = new DateTime(DateTime.UtcNow.Ticks).Date;
            var end = new DateTime(DateTime.UtcNow.Ticks).Date.AddHours(12).AddMilliseconds(-1);
            var cyclists = (await _cyclistRepository.AllAsync())?.ToList() ?? new List<Cyclist>();
            cyclists = cyclists.Where(c => c.LicenseRegistrationDate >= start && c.LicenseRegistrationDate <= end).ToList();
            if(cyclists.Count > 0)
            {
                var body = new StringBuilder();

                cyclists.ForEach(c => body.AppendLine($"{c.FirstName} {c.LastName} --> {c.LicenseCode}"));
                body.AppendLine($"A total of {cyclists.Count()} cyclist/cylists have been registered last night");
                _emailService.SendEmailAsync(new EmailSetting() {
                    
                    To = new string[] { owner.Email },
                    Subject = "Cyclist resgitered last night report",
                    Body = body.ToString(),
                                    
                });
            }

        }

        #endregion
    }
}
