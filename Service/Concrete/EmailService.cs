using CryptoReporter.Model;
using CryptoReporter.Service.Contract;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CryptoReporter.Service.Concrete
{
    public class EmailService : IEmailService
    {

        private IOptions<EmailSettings> _settings;
        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings;
        }

        public async Task<bool> Send( MessageContent messageContent)
        {
            try
            {
                MailMessage ePosta = messageContent.GetMailMessage();
                ePosta.From = new MailAddress(_settings.Value.SenderEmail, _settings.Value.SenderName);
                ePosta.To.Add(_settings.Value.Recipient);

                ePosta.IsBodyHtml = true;

                SmtpClient smtp = new()
                {
                    Credentials = new NetworkCredential(_settings.Value.SenderName, _settings.Value.SenderPass),
                    Port = _settings.Value.Port,
                    Host = _settings.Value.Server,
                    EnableSsl = _settings.Value.IsSSL
                };

                if (smtp.Port == 587)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }

                await smtp.SendMailAsync(ePosta);

                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
        }
    }
}
