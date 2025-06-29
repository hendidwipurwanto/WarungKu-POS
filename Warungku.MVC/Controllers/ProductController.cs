using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Domain.DTOs;

namespace Warungku.MVC.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public ActionResult Index()
        {
            ViewBag.currentUser = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetProducts()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allProducts = await _productService.GetAllAsync();
            var totalRecordBeforeFiltered = allProducts.Count();


            if (!string.IsNullOrEmpty(searchValue))
            {
                allProducts = allProducts.Where(p =>
                    p.Name.ToLower().Contains(searchValue) ||
                    p.CategoryName.ToLower().Contains(searchValue) ||
                    p.Price.ToString().Contains(searchValue) ||
                    p.Stock.ToString().Contains(searchValue)
                ).ToList();
            }

            int totalRecordAfterFiltered = allProducts.Count();
            var data = allProducts.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = totalRecordBeforeFiltered,
                recordsFiltered = totalRecordAfterFiltered,
                data = data
            });
        }



        public async Task<ActionResult> Details(int id)
        {
            var response = await _productService.GetByIdAsync(id);
            return PartialView("_detailModal", response);
        }

        private async Task<List<SelectListItem>> PopulateCategory()
        {
            var categoryList = await _categoryService.GetAllAsync();
            var categories = new List<SelectListItem>();
            foreach (var item in categoryList)
            {
                var selectListItem = new SelectListItem { Value = item.Id.ToString(), Text = item.Name };
                categories.Add(selectListItem);
            }
            return categories;
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var categories = await PopulateCategory();
            var model = new ProductRequest();
           model.Categories = new List<SelectListItem>();
            model.Categories.AddRange(categories);

             return PartialView("_addModal", model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductRequest request)
        {
        if (ModelState.IsValid)
        {
                var response = await _productService.CreateAsync(request);


        return Json(new { success = true, message = "Data Saved Successfully!" });
        }


        return PartialView("_addModal", request);
}

        public async Task<ActionResult> Edit(int id)
        {
            var categories = await PopulateCategory();
            var response = await _productService.GetByIdAsync(id);
            response.Categories = new List<SelectListItem>();
            response.Categories.AddRange(categories);
            

            return PartialView("_editModal", response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductRequest request)
        {
            if (ModelState.IsValid)
            {
                var model = await _productService.UpdateAsync(id, request);

                return Json(new { success = true, message = "Data Saved Successfully!" });
            }
            var response = new ProductResponse() { Name = request.Name, Price=Convert.ToDecimal(request.Price), CategoryId=3,  Stock = Convert.ToInt32(request.Stock) };



            return PartialView("_editModal", response);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _productService.GetByIdAsync(id);
            if (model != null)
            {
                var response = await _productService.DeleteAsync(id);
                return Json(new { success = response, message = "Data Deleted Successfully!" });
            }

            return Json(new { success = true, message = "Data Deleted Successfully!" });
        }

        
    }
}
