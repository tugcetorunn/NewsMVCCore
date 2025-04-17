using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCCore11IdentityUygulama.Areas.AdminArea.Controllers
{
    // 22. arealar için gerekli controller ların oluşturulması
    [Area("AdminArea")]
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
