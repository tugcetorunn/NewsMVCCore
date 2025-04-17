using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCCore11IdentityUygulama.ViewModels.Haberler;

namespace MVCCore11IdentityUygulama.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class HaberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
