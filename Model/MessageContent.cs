using System.Net.Mail;

namespace CryptoReporter.Model
{
    public class MessageContent
    {
        public MessageContent(string subject, string body)
        {
            Subject = subject;
            Body = body;
        }

        public string Subject { get; set; }
        public string Body { get; set; }

        public MailMessage GetMailMessage()
        {
            var mailMessage = new MailMessage
            {
                Subject = this.Subject,
                Body = this.Body
            };
            return mailMessage;
        }
    }
}
