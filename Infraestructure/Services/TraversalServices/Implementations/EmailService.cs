using System.Net;
using System.Net.Mail;
using TraversalServices.Interfaces;
using TraversalServices.Models;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Options;

namespace TraversalServices.Implementations
{
    public class EmailService : IEmailService
    {
        #region private properties

        private string _smtpServer;
        private short _smtpPort;
        private string _fromAddress;
        private string _fromPassword;
        private string _ownerEmail;

        #endregion
             

        #region constructor

        public EmailService(IOptions<EmailSection> emailSection)
        {
            _smtpServer = emailSection.Value.SmtpServer;
            _smtpPort = emailSection.Value.SmtpPort;
            _fromAddress = emailSection.Value.From;
            _fromPassword = emailSection.Value.Password;            
        }

        #endregion

        #region public methods

        public async void SendEmailAsync(EmailSetting settings)
        {
            GetSmtpClient().SendAsync(GetMailMessage(settings), new { });
        }

        #endregion

        #region private methods

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Host = _smtpServer,
                Port = _smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_fromAddress, _fromPassword)
            };
        }

        private MailMessage GetMailMessage(EmailSetting settings)
        {
            var message = new MailMessage()
            {
                From = new MailAddress(_fromAddress),
                Subject = settings.Subject,
                Body = settings.Body,
                IsBodyHtml = false
            };
            message.To.Add(string.Join(",", settings.To));
            settings.Attachments.ToList().ForEach(att => message.Attachments.Add(new Attachment(new MemoryStream(att.Value), att.Key)));
            return message;
        }

        #endregion
    }
}
