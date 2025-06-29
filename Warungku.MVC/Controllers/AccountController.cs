using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Domain.DTOs;

namespace Warungku.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.LoginAsync(model);
            if (result.Succeeded)
         
                return RedirectToAction("Index", "Dashboard");

            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        public async Task<IActionResult> Signout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Signin");
        }

        public ActionResult Register()
        {
            return View(new RegisterRequest());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.RegisterAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Index", "Confirmation");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
 
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
