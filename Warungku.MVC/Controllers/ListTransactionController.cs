using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Application.Services;
using Warungku.Core.Domain.DTOs;

namespace Warungku.MVC.Controllers
{

    public class ListTransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        public ListTransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public ActionResult Index()
        {
            ViewBag.currentUser = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTransactions()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null && Convert.ToInt32(length) > 0 ? Convert.ToInt32(length) : 10;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allTransactions = await _transactionService.GetAllAsync();
            var totalRecordBeforeFiltered = allTransactions.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                allTransactions = allTransactions.Where(p =>
                    p.Date.ToLower().Contains(searchValue) ||
                    p.Discount.ToString().Contains(searchValue) ||
                    p.GrandTotal.ToString().Contains(searchValue) ||
                    p.User.ToLower().Contains(searchValue) ||
                    p.Voucher.ToString().Contains(searchValue)
                ).ToList();
            }

            int totalRecordAfterFiltered = allTransactions.Count();
            var data = allTransactions.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = totalRecordBeforeFiltered,
                recordsFiltered = totalRecordAfterFiltered,
                data = data
            });
        }

        // GET: ListTransactionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListTransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListTransactionController/Create
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

        // GET: ListTransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListTransactionController/Edit/5
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

        // GET: ListTransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListTransactionController/Delete/5
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
