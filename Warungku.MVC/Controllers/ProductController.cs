using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warungku.MVC.Models;

namespace Warungku.MVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProducts()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allProducts = new List<ProductResponse>();
            for (int i = 1; i <= 1000; i++)
            {
                allProducts.Add(new ProductResponse
                {
                    Id = i,
                    Name = $"Product {i}",
                    Stock = 10 + i,
                    Price = 10000 + (i * 100),
                    Category = "Food"
                });
            }

       
            if (!string.IsNullOrEmpty(searchValue))
            {
                allProducts = allProducts.Where(p =>
                    p.Name.ToLower().Contains(searchValue) ||
                    p.Category.ToLower().Contains(searchValue) ||
                    p.Price.ToString().Contains(searchValue) ||
                    p.Stock.ToString().Contains(searchValue)
                ).ToList();
            }

            int totalRecords = allProducts.Count;
            var data = allProducts.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = 1000, // total before filtered
                recordsFiltered = totalRecords, // total after filtered
                data = data
            });
        }



        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Makanan" },
                new SelectListItem { Value = "2", Text = "Minuman" }
            };
            
            var model = new ProductRequest();
           model.Categories = new List<SelectListItem>();
            model.Categories.AddRange(categories);

             return PartialView("_productModal", model);
        }

        // POST: CategoriesController/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(ProductRequest request)
{
    if (ModelState.IsValid)
    {

        return Json(new { success = true, message = "Data berhasil disimpan!" });
    }


    return PartialView("_productModal", request);
}

        // GET: ProductController/Edit/5 
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
