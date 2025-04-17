using System.ComponentModel.DataAnnotations;

namespace MVCCore11IdentityUygulama.ViewModels.Haberler
{
    public class HaberEkleVM
    {
        public int HaberId { get; set; }
        [Display(Name = "Haber Başlığı")]
        public string Baslik { get; set; }
        public string Detay { get; set; }
        [Display(Name = "Resim Dosyası")]
        public IFormFile ResimDosyasi { get; set; }
        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }
    }
}