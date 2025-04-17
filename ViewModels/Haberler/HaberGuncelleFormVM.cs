using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCore11IdentityUygulama.ViewModels.Haberler
{
    public class HaberGuncelleFormVM
    {
        public SelectList Kategoriler { get; set; }
        public HaberGuncelleVM Haber { get; set; }
    }
}
