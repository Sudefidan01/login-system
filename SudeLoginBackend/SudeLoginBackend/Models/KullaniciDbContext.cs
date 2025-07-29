using Microsoft.EntityFrameworkCore;

namespace SudeLoginBackend.Models
{
    public class KullaniciDbContext : DbContext
    {
        public KullaniciDbContext() { }

        public KullaniciDbContext(DbContextOptions<KullaniciDbContext> options) : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}