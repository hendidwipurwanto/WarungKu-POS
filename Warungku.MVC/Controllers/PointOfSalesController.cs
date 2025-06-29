using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warungku.MVC.Models;

namespace Warungku.MVC.Controllers
{
    public class PointOfSalesController : Controller
    {
        // GET: PointOfSalesController
        public ActionResult Index()
        {
            ViewBag.currentUser = User.Identity.Name;
            return View();
        }
        [HttpPost]
        public JsonResult GetPoses()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null && Convert.ToInt32(length) > 0 ? Convert.ToInt32(length) : 10;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allPoses = new List<PosView>();
            for (int i = 1; i <= 1000; i++)
            {
                allPoses.Add(new PosView
                {
                    Id = i,
                    Item= "Indomie-"+i,
                      Quantity= i,
                       Subtotal = 10000 + (i * 100)
                });
            }


            if (!string.IsNullOrEmpty(searchValue))
            {
                allPoses = allPoses.Where(p =>
                    p.Item.ToLower().Contains(searchValue) ||
                    p.Quantity.ToString().ToLower().Contains(searchValue) ||
                    p.Subtotal.ToString().Contains(searchValue)
                ).ToList();
            }

            int totalRecords = allPoses.Count;
            var data = allPoses.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = 1000, // total before filtered
                recordsFiltered = totalRecords, // total after filtered
                data = data
            });
        }

        // GET: PointOfSalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PointOfSalesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PointOfSalesController/Create
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

        // GET: PointOfSalesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PointOfSalesController/Edit/5
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

        // GET: PointOfSalesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PointOfSalesController/Delete/5
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
