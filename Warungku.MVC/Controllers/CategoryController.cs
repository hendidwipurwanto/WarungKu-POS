using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Warungku.MVC.Models;

namespace Warungku.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private static readonly string[] Status = { "Active", "Inactive", "Draft" };
        private static readonly Random _random = new Random();
        // GET: CategoriesController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCategories()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null && Convert.ToInt32(length) > 0 ? Convert.ToInt32(length) : 10;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allCategories = new List<CategoryResponse>();
            for (int i = 1; i <= 1000; i++)
            {
                allCategories.Add(new CategoryResponse
                {
                    Id = i,
                    Name = $"Category {i}",
                    Description = " this is description-" + i,
                    Status = Status[_random.Next(Status.Length)]
                });
            }


            if (!string.IsNullOrEmpty(searchValue))
            {
                allCategories = allCategories.Where(p =>
                    p.Name.ToLower().Contains(searchValue) ||
                    p.Status.ToLower().Contains(searchValue) ||
                    p.Description.ToString().Contains(searchValue)
                ).ToList();
            }

            int totalRecords = allCategories.Count;
            var data = allCategories.Skip(skip).Take(pageSize).ToList();

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
            return PartialView("_detailModal", new CategoryResponse());
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
        public ActionResult Create(CategoryRequest request)
        {
            if (ModelState.IsValid)
            {

                return Json(new { success = true, message = "Data Saved Successfully!" });
            }


            return PartialView("_addModal", request);
        }

        
        public ActionResult Edit(int id)
        {
            return PartialView("_editModal", new CategoryResponse() { Name="testing", Description="wakaka", Status="Active"});
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryRequest request)
        {
            if (ModelState.IsValid)
            {

                return Json(new { success = true, message = "Data Saved Successfully!" });
            }
            var response = new CategoryResponse() { Name = request.Name, Description = request.Description };
           


            return PartialView("_editModal", response);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
