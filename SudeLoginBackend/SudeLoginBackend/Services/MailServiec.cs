using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SudeLoginBackend.Services
{
    public class MailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "sudefidan2004@gmail.com"; 
        private readonly string _smtpPass = "nlqmohzdjdkjxehk"; 

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(_smtpUser, "SudeLoginBackend");
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(mail);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}