using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCore11IdentityUygulama.Data;
using MVCCore11IdentityUygulama.Models;
using MVCCore11IdentityUygulama.Utilities;
using MVCCore11IdentityUygulama.ViewModels.Haberler;
using System.Threading.Tasks;

namespace MVCCore11IdentityUygulama.Controllers
{
    // 22. haber işlemleri için gerekli controller ların oluşturulması
    public class HaberController : Controller
    {
        private readonly HaberPortalDbContext dbContext;
        private readonly UserManager<Editor> userManager;
        public HaberController(HaberPortalDbContext _dbContext, UserManager<Editor> _userManager)
        {
            dbContext = _dbContext;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            var haberler = dbContext.Haberler.Select(x => new HaberListeleVM
            {
                HaberId = x.HaberId,
                Baslik = x.Baslik,
                ResimYolu = x.ResimYolu,
                OlusturulmaTarihi = x.OlusturulmaTarihi,
                Kategori = x.Kategori.KategoriAdi,
                Editor = x.Editor.AdSoyad
            }).ToList();

            return View(haberler);
        }

        public IActionResult Detay(int id)
        {
            var haber = dbContext.Haberler
                .Where(x => x.HaberId == id)
                .Select(x => new HaberDetayVM
                {
                    HaberId = x.HaberId,
                    Baslik = x.Baslik,
                    Detay = x.Detay,
                    ResimYolu = x.ResimYolu,
                    OlusturulmaTarihi = x.OlusturulmaTarihi,
                    Kategori = x.Kategori.KategoriAdi,
                    Editor = x.Editor.AdSoyad
                })
                .FirstOrDefault();

            return View(haber);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult HaberEkle()
        {
            HaberEkleFormVM frm = new HaberEkleFormVM() { Kategoriler = KategoriSelectListOlustur() };
            return View(frm);
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult HaberEkle(HaberEkleVM haber)
        {
            if (!ModelState.IsValid)
            {
                HaberEkleFormVM frm = new HaberEkleFormVM() { Kategoriler = KategoriSelectListOlustur() };
                return View(frm);
            }
            Haber yeniHaber = new Haber
            {
                Baslik = haber.Baslik,
                Detay = haber.Detay,
                EditorId = int.Parse(userManager.GetUserId(User)),
                KategoriId = haber.KategoriId,
                OlusturulmaTarihi = DateTime.Now,
                ResimYolu = FileOperations.UploadImage(haber.ResimDosyasi),
            };

            dbContext.Haberler.Add(yeniHaber);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Editor")]
        public IActionResult HaberGuncelle(int id)
        {
            var haber = dbContext.Haberler
                .Where(x => x.HaberId == id)
                .Select(x => new HaberGuncelleVM
                {
                    HaberId = x.HaberId,
                    Baslik = x.Baslik,
                    Detay = x.Detay,
                    ResimYolu = x.ResimYolu,
                    KategoriId = x.KategoriId
                }).FirstOrDefault();
            var frm = new HaberGuncelleFormVM
            {
                Kategoriler = KategoriSelectListOlustur(),
                Haber = haber
            }; // değerler gelsin
            return View(frm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> HaberGuncelle(HaberGuncelleVM haber) // ekle, düzenle veya sil linklerine tıklandığında eğer editör ya da üye değilse logine göndersin hata vermesin...
        {
            if (!ModelState.IsValid) // resim dosyası null olursa ama nedeni de önceki dosyayı kullanmak istemekse url yeni habere eklenmeli ama sadece bu yetmiyor. dosya null old isvalid false geliyor nullable olsa da. bu sebeple cshtml de dosya validationı url ile sağlarsak sıkıntı ortadan kalkıyor. 
            {
                var frm = new HaberGuncelleFormVM
                {
                    Kategoriler = KategoriSelectListOlustur(),
                    Haber = haber
                };
                return View(frm);
            }

            var user = await userManager.GetUserAsync(User);

            // eğer adminse her haberi tüm detaylarıyla güncelleyebilir, eğer editörse sadece kendi haberini güncelleyebilir.

            if (GuncellemeIcınYetkiliMi(haber.HaberId))
            {
                var eskihaber = dbContext.Haberler.Find(haber.HaberId);
                if (eskihaber == null)
                {
                    ModelState.AddModelError("", "Haber bulunamadı.");
                    return RedirectToAction("Index");
                }

                eskihaber.Baslik = haber.Baslik;
                eskihaber.Detay = haber.Detay;
                eskihaber.KategoriId = haber.KategoriId;
                eskihaber.ResimYolu = haber.ResimYolu;

                if (haber.ResimDosyasi != null)
                {
                    // FileOperations.DeleteImage(eskihaber.ResimYolu);
                    eskihaber.ResimYolu = FileOperations.UploadImage(haber.ResimDosyasi);
                }

                dbContext.Haberler.Update(eskihaber);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Bu haberi güncelleyemezsiniz. Yetkiniz yok.");
                return RedirectToAction("Login", "Auth");
            }
        }

        public SelectList KategoriSelectListOlustur()
        {
            return new SelectList(dbContext.Kategoriler.ToList(), "KategoriId", "KategoriAdi");
        }

        public bool GuncellemeIcınYetkiliMi(int haberId)
        {
            //var user = userManager.GetUserAsync(User).Result; bu şekilde yaptığımızda haberler gelmiyor include edilmeli.
            var users = userManager.Users.Include(x => x.Haberler).ToList();
            var user = users.FirstOrDefault(x => x.Id == int.Parse(userManager.GetUserId(User)));

            if(User.IsInRole("Admin") || User.IsInRole("Editor") && user.Haberler.FirstOrDefault(x => x.HaberId == haberId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Sil(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                ModelState.AddModelError("", "Yetkiniz yok.");
                return RedirectToAction("Index");
            }
            var haber = dbContext.Haberler.Find(id);
            if (haber != null)
            {
                dbContext.Haberler.Remove(haber);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Haber bulunamadı.");
            return RedirectToAction("Index");
        }
    }
}
