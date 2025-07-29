namespace SudeLoginBackend.Models
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        public string? KullaniciName { get; set; }
        public string? KullaniciPassword { get; set; }

        public string? KullaniciEmail {  get; set; }

        public string? KullaniciRol { get; set; }
        public DateTime? OlusturmaTarihi { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
        public bool AktifMi {  get; set; }


    }
}
