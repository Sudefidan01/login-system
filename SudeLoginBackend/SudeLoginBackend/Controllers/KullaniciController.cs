using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SudeLoginBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SudeLoginBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KullaniciController : ControllerBase
    {
        private readonly KullaniciDbContext _context;
        private readonly IConfiguration _configuration;

        public KullaniciController(KullaniciDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] Kullanici giris)
        {
            var kullanici = _context.Kullanicilar
                .FirstOrDefault(k =>
                    k.KullaniciName == giris.KullaniciName &&
                    k.KullaniciPassword == giris.KullaniciPassword &&
                    k.AktifMi == true);

            if (kullanici == null)
            {
                return Unauthorized(new { mesaj = "Kullanıcı adı, şifre hatalı veya kullanıcı pasif." });
            }

            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, kullanici.KullaniciId.ToString()),
                new Claim("kullaniciName", kullanici.KullaniciName ?? ""),
                new Claim("kullaniciEmail", kullanici.KullaniciEmail ?? ""),
                new Claim("kullaniciRol", kullanici.KullaniciRol ?? ""),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiresInMinutes"])),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            Response.Cookies.Append("jwt", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(60)
            });

           
            return Ok(new
            {
                mesaj = "Giriş başarılı, token cookie'ye yazıldı.",
                token = tokenString, 
                kullanici = new
                {
                    kullanici.KullaniciId,
                    kullanici.KullaniciName,
                    kullanici.KullaniciEmail,
                    kullanici.KullaniciRol,
                    kullanici.OlusturmaTarihi,
                    kullanici.GuncellenmeTarihi,
                    kullanici.AktifMi
                }
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetKullanici(int id)
        {
            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.KullaniciId == id);

            if (kullanici == null)

            {
                return NotFound(new { mesaj = "Kullanıcı bulunamadı." });

            }
            return Ok(kullanici);
        }

		[Authorize]
		[HttpGet("me")]
		public IActionResult GetCurrentUser()
		{
			var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int id))
				return Unauthorized();

			var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.KullaniciId == id);
			if (kullanici == null) return NotFound();

			return Ok(kullanici);
		}


	}
}
