using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCCore11IdentityUygulama.ViewModels.Haberler
{
    // 23. haber ekleme formu için gerekli viewmodel
    public class HaberEkleFormVM
    {
        public SelectList Kategoriler { get; set; }
        public HaberEkleVM Haber { get; set; }
    }
}
