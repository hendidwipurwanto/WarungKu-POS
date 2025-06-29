using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Domain.DTOs;


namespace Warungku.MVC.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.currentUser = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetCategories()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null && Convert.ToInt32(length) > 0 ? Convert.ToInt32(length) : 10;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allCategories = await _categoryService.GetAllAsync();
            var totalRecordBeforeFiltered = allCategories.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                allCategories = allCategories.Where(p =>
                    p.Name.ToLower().Contains(searchValue) ||
                    p.Status.ToLower().Contains(searchValue) ||
                    p.Description.ToString().Contains(searchValue)
                ).ToList();
            }

            int totalRecordAfterFiltered = allCategories.Count();
            var data = allCategories.Skip(skip).Take(pageSize).ToList();

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
             var response = await _categoryService.GetByIdAsync(id);


            return PartialView("_detailModal", response);
        }

        // GET: CategoriesController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_addModal", new CategoryRequest());
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_addModal", request);
            }
            var result = await _categoryService.CreateAsync(request);
            if (result == null)
            {
                ModelState.AddModelError("", "Failed to create category");
                return View(request);
            }

            return Json(new { success = true, message = "Data added Successfully!" });


        }

        
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);

            return PartialView("_editModal", response);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                var model = await _categoryService.UpdateAsync(id, request);


                return Json(new { success = true, message = "Data Saved Successfully!" });
            }
            var response = new CategoryResponse() { Name = request.Name, Description = request.Description };
           


            return PartialView("_editModal", response);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _categoryService.GetByIdAsync(id);
            if(model != null)
            {
                var response = await _categoryService.DeleteAsync(id);
                return Json(new { success = response, message = "Data Deleted Successfully!" });
            }

            return Json(new { success = true, message = "Data Deleted Successfully!" });
        }
    }
}
