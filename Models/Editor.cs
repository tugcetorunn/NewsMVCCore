using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCore11IdentityUygulama.Models
{
    public class Editor : IdentityUser<int>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [NotMapped]
        public string AdSoyad => $"{Ad} {Soyad}";
        public ICollection<Haber> Haberler { get; set; }
    }
}
