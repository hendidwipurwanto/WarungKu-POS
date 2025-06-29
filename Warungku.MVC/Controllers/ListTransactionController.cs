using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warungku.MVC.Models;

namespace Warungku.MVC.Controllers
{

    public class ListTransactionController : Controller
    {
        // GET: ListTransactionController
        public ActionResult Index()
        {
            ViewBag.currentUser = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public JsonResult GetTransactions()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null && Convert.ToInt32(length) > 0 ? Convert.ToInt32(length) : 10;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allTransactions = new List<TransactionResponse>();
            for (int i = 1; i <= 1000; i++)
            {
                allTransactions.Add(new TransactionResponse
                {
                    Id = i, 
                    Date = DateTime.Now.ToString("dd-MMMM-yyyy"),
                     Discount = i+5,
                      GrandTotal= (10 * i),
                       Total = 10000 + (i * 100),
                        User= "Staff-"+i, 
                    Voucher = 10+ i,                   
                });
            }


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

            int totalRecords = allTransactions.Count;
            var data = allTransactions.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = 1000, // total before filtered
                recordsFiltered = totalRecords, // total after filtered
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
