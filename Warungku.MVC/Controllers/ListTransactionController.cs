using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Warungku.MVC.Controllers
{
    public class ListTransactionController : Controller
    {
        // GET: ListTransactionController
        public ActionResult Index()
        {
            return View();
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
