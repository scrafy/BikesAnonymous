using TraversalServices.Models;

namespace TraversalServices.Interfaces
{
    public interface IEmailService
    {
        void SendEmailAsync(EmailSetting settings);
        
    }
}
