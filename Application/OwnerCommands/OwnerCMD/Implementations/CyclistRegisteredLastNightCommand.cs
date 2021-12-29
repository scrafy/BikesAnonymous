using Core.Entities;
using DataLayer.Interfaces;
using OwnerCMD.Interfaces;
using System.Threading.Tasks;
using TraversalServices.Interfaces;
using System.Linq;
using System;
using TraversalServices.Models;
using System.Text;

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

        public CyclistRegisteredLastNightCommand(IOwnerRepository<Owner> ownerRepository, ICyclistRepository<Cyclist> cyclistRepository, IEmailService emailService)
        {
            _cyclistRepository = cyclistRepository;
            _ownerRepository = ownerRepository;
            _emailService = emailService;
        }

        #endregion

        #region public methods

        public async Task GetCylistRegisteredLastNight()
        {
            var owner = (await _ownerRepository.AllAsync()).FirstOrDefault();

            if (owner == null)
                throw new Exception("Any owner account has been registered");

            var start = new DateTime(DateTime.UtcNow.Ticks).Date;
            var end = new DateTime(DateTime.UtcNow.Ticks).Date.AddHours(12).AddMilliseconds(-1);
            var cyclists = (await _cyclistRepository.AllAsync()).ToList();
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
