using Microsoft.AspNetCore.Mvc;
using SudeLoginBackend.Services;
using System.Threading.Tasks;

namespace SudeLoginBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly MailService _mailService;

        public NotificationController()
        {
            _mailService = new MailService();
        }

        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMail([FromQuery] string email)
        {
            var subject = "Hoşgeldin!";
            var body = "<h2>Merhaba!</h2><p>Projeye başarıyla kayıt oldunuz.</p>";

            bool result = await _mailService.SendEmailAsync(email, subject, body);
            return result ? Ok("Mail gönderildi.") : BadRequest("Mail gönderilemedi.");
        }
    }
}
