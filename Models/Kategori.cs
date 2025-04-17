namespace MVCCore11IdentityUygulama.Models
{
    public class Kategori
    {
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public List<Haber>? Haberler { get; set; }
    }
}
