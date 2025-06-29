using Microsoft.AspNetCore.Mvc;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Domain.DTOs;

namespace Warungku.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _authService.LoginAsync(request);
            if (result == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(request);
            }

            // Set session atau cookie untuk authentication
            HttpContext.Session.SetString("UserId", result.Id.ToString());
            HttpContext.Session.SetString("Username", result.Username);
            HttpContext.Session.SetString("Role", result.Role);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            if (await _authService.UserExistsAsync(request.Username, request.Email))
            {
                ModelState.AddModelError("", "Username or email already exists");
                return View(request);
            }

            var result = await _authService.RegisterAsync(request);
            if (result == null)
            {
                ModelState.AddModelError("", "Registration failed");
                return View(request);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
