namespace MVCCore11IdentityUygulama.Models
{
    // 1. gerekli paketlerin yüklenmesi
    // 2. modellerin oluşturulması
    public class Haber
    {
        public int HaberId { get; set; }
        public string Baslik { get; set; }
        public string Detay { get; set; }
        public string ResimYolu { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public int KategoriId { get; set; }
        public Kategori? Kategori { get; set; }
        public int EditorId { get; set; }
        public Editor? Editor { get; set; }
    }
}
