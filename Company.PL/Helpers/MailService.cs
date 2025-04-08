using Company.PL.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Company.PL.Helpers
{
    public class MailService(IOptions<MailSettings> _options) : IMailService
    {
       
        public void SendEamil(Email email)
        {
            // bulit Message 

            var mail = new MimeMessage();

            mail.Subject = email.Subject;
            mail.From.Add(new  MailboxAddress(_options.Value.DisplayName,_options.Value.Email));
            mail.To.Add(MailboxAddress.Parse(email.To));

            var bulider = new BodyBuilder();

            bulider.TextBody = email.Body;

            mail.Body = bulider.ToMessageBody();


            // Establish Connection

            using var smpt = new SmtpClient();

            var host = _options.Value.Host;
            var port = _options.Value.port;
            smpt.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);

            smpt.Authenticate(_options.Value.Email, _options.Value.password);


            // Send Message

            smpt.Send(mail);




        }
    }
}
