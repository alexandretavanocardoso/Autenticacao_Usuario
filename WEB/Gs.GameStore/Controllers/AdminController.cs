using Microsoft.AspNetCore.Mvc;

namespace Gs.GameStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
