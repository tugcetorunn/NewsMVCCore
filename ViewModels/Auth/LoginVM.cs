using System.ComponentModel.DataAnnotations;

namespace MVCCore11IdentityUygulama.ViewModels.Auth
{
    // 17. auth işlemleri için vm oluşturulması
    public class LoginVM
    {
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
