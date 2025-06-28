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
                    CategoryName = "Food"
                });
            }

       
            if (!string.IsNullOrEmpty(searchValue))
            {
                allProducts = allProducts.Where(p =>
                    p.Name.ToLower().Contains(searchValue) ||
                    p.CategoryName.ToLower().Contains(searchValue) ||
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



        public ActionResult Details(int id)
        {
            return PartialView("_detailModal", new ProductResponse() { CategoryName="test", Name="testing", Price=1000, Stock=20});
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Food" },
                new SelectListItem { Value = "2", Text = "Beverage" },
                 new SelectListItem { Value = "3", Text = "Snack" }
            };
            
            var model = new ProductRequest();
           model.Categories = new List<SelectListItem>();
            model.Categories.AddRange(categories);

             return PartialView("_addModal", model);
        }

        // POST: CategoriesController/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(ProductRequest request)
{
    if (ModelState.IsValid)
    {

        return Json(new { success = true, message = "Data Saved Successfully!" });
    }


    return PartialView("_addModal", request);
}

        public ActionResult Edit(int id)
        {
            var categories = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Food" },
                new SelectListItem { Value = "2", Text = "Beverage" },
                 new SelectListItem { Value = "3", Text = "Snack" }
            };

            return PartialView("_editModal", new ProductResponse() { Name = "testing", Price = 1000, Stock = 30 , CategoryId= 3, Categories=categories});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductRequest request)
        {
            if (ModelState.IsValid)
            {

                return Json(new { success = true, message = "Data Saved Successfully!" });
            }
            var response = new ProductResponse() { Name = request.Name, Price=Convert.ToDecimal(request.Price), CategoryId=3,  Stock = Convert.ToInt32(request.Stock) };



            return PartialView("_editModal", response);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        
    }
}
