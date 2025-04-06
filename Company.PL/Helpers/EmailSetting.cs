using System.Net;
using System.Net.Mail;

namespace Company.PL.Helpers
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            // Mail Server : Gmail
            // SMTP 
            // Pass@word12
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;

                client.Credentials = new NetworkCredential("mo7m3d1235@gmail.com", "karhwjezsvfktsdx");
                client.Send("mo7m3d1235@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
