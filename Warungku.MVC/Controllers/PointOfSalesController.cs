using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Warungku.MVC.Controllers
{
    public class PointOfSalesController : Controller
    {
        // GET: PointOfSalesController
        public ActionResult Index()
        {
            return View();
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
