using Microsoft.AspNetCore.Mvc;

namespace Warungku.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            if (statusCode == 403)
                return View("403");
            else if (statusCode == 404)
                return View("404");
            else
                return View("500"); // fallback
        }

        [Route("Error")]
        public IActionResult GeneralError()
        {
            return View("500");
        }
    }

}
