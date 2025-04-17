using System.ComponentModel.DataAnnotations;

namespace MVCCore11IdentityUygulama.ViewModels.Auth
{
    public class RegisterVM
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Display(Name = "Eposta")]
        public string Email { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Şifre Tekrarı")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
