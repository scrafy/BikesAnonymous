using System.Threading.Tasks;
using TraversalServices.Models;

namespace TraversalServices.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailSettings settings);
        
    }
}
