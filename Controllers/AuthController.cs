using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCCore11IdentityUygulama.Data;
using MVCCore11IdentityUygulama.Models;
using MVCCore11IdentityUygulama.ViewModels.Auth;
using System.Threading.Tasks;

namespace MVCCore11IdentityUygulama.Controllers
{
    // 15. authorize işlemleri için controllerın oluşturulması
    public class AuthController : Controller
    {
        // 16. login işlemi için di lar
        private readonly UserManager<Editor> userManager;
        private readonly SignInManager<Editor> signInManager;
        private readonly HaberPortalDbContext dbContext;
        public AuthController(UserManager<Editor> _userManager, SignInManager<Editor> _signInManager, HaberPortalDbContext _dbContext)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            dbContext = _dbContext;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            var user = await userManager.FindByNameAsync(vm.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(vm);
            }

            var result = await signInManager.PasswordSignInAsync(user, vm.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Şifre hatalı.");
                return View(vm);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Editor yeniEditor = new Editor
            {
                Ad = vm.Ad,
                Soyad = vm.Soyad,
                UserName = vm.Username,
                Email = vm.Email
            };

            yeniEditor.PasswordHash = userManager.PasswordHasher.HashPassword(yeniEditor, vm.Password);

            await userManager.CreateAsync(yeniEditor, vm.Password);
            await userManager.AddToRoleAsync(yeniEditor, "Editor");

            return RedirectToAction("Login");
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
