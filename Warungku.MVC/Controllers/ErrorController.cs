using Microsoft.AspNetCore.Mvc;

namespace Warungku.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult Error404()
        {
            return View("Error404");
        }

        [Route("Error/403")]
        public IActionResult Error403()
        {
            return View("Error403");
        }

        [Route("Error/500")]
        public IActionResult Error500()
        {
            return View("Error500");
        }
    }

}
