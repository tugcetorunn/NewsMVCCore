using System.ComponentModel.DataAnnotations;

namespace MVCCore11IdentityUygulama.ViewModels.Haberler
{
    public class HaberGuncelleVM
    {
        public int HaberId { get; set; }
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }
        public string Detay { get; set; }
        public string ResimYolu { get; set; }
        [Display(Name = "Resim")]
        public IFormFile? ResimDosyasi { get; set; }
        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }
    }
}
