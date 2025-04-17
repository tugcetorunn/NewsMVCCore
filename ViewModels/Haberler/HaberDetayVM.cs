using System.ComponentModel.DataAnnotations;

namespace MVCCore11IdentityUygulama.ViewModels.Haberler
{
    public class HaberDetayVM
    {
        public int HaberId { get; set; }
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }
        [Display(Name = "Detay")]
        public string Detay { get; set; }
        [Display(Name = "Resim")]
        public string ResimYolu { get; set; }
        [Display(Name = "Haber Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OlusturulmaTarihi { get; set; }
        public string Kategori { get; set; }
        [Display(Name = "Editör")]
        public string Editor { get; set; }
    }
}
