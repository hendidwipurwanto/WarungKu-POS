using Microsoft.AspNetCore.Mvc;

namespace Warungku.MVC.Controllers
{
    public class ConfirmationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
