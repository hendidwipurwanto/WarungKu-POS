using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Application.Services;
using Warungku.Core.Domain.DTOs;
using Warungku.MVC.Models;

namespace Warungku.MVC.Controllers
{
    public class PointOfSalesController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPointOfSaleService _pointOfSaleService;
        private readonly ITransactionService _transactionService;
        public PointOfSalesController(IProductService productService, IPointOfSaleService pointOfSaleService,ITransactionService transactionService)
        {
            _productService = productService;
            _pointOfSaleService = pointOfSaleService;
            _transactionService = transactionService;
        }
        public async Task<ActionResult> Index()
        {
            var model = new PosRequest();
            ViewBag.currentUser = User.Identity.Name;
            var productList = await _productService.GetAllAsync();
            model.Products = new List<SelectListItem>();
            foreach (var item in productList)
            {
                var temp = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };
                model.Products.Add(temp);
            }
          
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> GetPoses()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null && Convert.ToInt32(length) > 0 ? Convert.ToInt32(length) : 10;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allPoses = await _pointOfSaleService.GetAllAsync();
            var totalRecordBeforeFiltered = allPoses.Count();


            if (!string.IsNullOrEmpty(searchValue))
            {
                allPoses = allPoses.Where(p =>
                    p.Item.ToLower().Contains(searchValue) ||
                    p.Quantity.ToString().ToLower().Contains(searchValue) ||
                    p.Subtotal.ToString().Contains(searchValue)
                ).ToList();
            }

            int totalRecordAfterFiltered = allPoses.Count();
            var data = allPoses.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = totalRecordBeforeFiltered, // total before filtered
                recordsFiltered = totalRecordAfterFiltered, // total after filtered
                data = data
            });
        }

        // GET: PointOfSalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PointOfSalesController/Create
        public ActionResult CreateTransaction()
        {
            return PartialView("_addModal", new TransactionRequest());
        }

        // POST: PointOfSalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTransaction(TransactionRequest request)
        {
            request.User = User.Identity.Name;
            request.Date = DateTime.Now;
           
                var result = await _transactionService.CreateAsync(request);

            // return PartialView("_addModal", new TransactionRequest());

            //}

            return RedirectToAction("index");
            
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
