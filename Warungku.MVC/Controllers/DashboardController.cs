using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Domain.Entities;

namespace Warungku.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;
        public DashboardController(UserManager<ApplicationUser> userManager, IProductService productService, ICategoryService categoryService, ITransactionService transactionService)
        {
            _userManager = userManager;
            _productService = productService;
            _categoryService = categoryService;
            _categoryService = categoryService;
            _transactionService = transactionService;
        }
        public async Task<ActionResult> Index()
        {
            var transactions = await _transactionService.GetAllAsync();
            int totalUsers = _userManager.Users.Count();
            var products = await _productService.GetAllAsync();
            var category = await _categoryService.GetAllAsync();
            ViewBag.totalSales = transactions.Sum(s => s.GrandTotal);
            ViewBag.totalCategory = category.Count();
            ViewBag.TotalProduct = products.Count();
            ViewBag.TotalUsers = totalUsers;
            ViewBag.currentUser = User.Identity.Name;
            return View();
        }

        // GET: DashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: DashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashboardController/Edit/5
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

        // GET: DashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardController/Delete/5
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
