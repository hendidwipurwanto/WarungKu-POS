using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Warungku.MVC.Models;

namespace Warungku.MVC.Controllers
{
    public class UserManagementController : Controller
    {
        private static readonly string[] Status = { "Active", "Inactive", "Draft" };
        private static readonly Random _statusRandom = new Random();
        private static readonly string[] Roles = { "Admin", "Staff", "Manager" };
        private static readonly Random _roleRandom = new Random();
        // GET: UserManagementController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUsers()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allProducts = new List<UserResponse>();
            for (int i = 1; i <= 1000; i++)
            {
                allProducts.Add(new UserResponse
                {
                    Id = i,
                     Email ="user"+i+"@mail.com",
                    Role = Roles[_roleRandom.Next(Roles.Length)],
                     Status = Status[_statusRandom.Next(Status.Length)],
                      UserName ="user"+i,
                       LastLogin= DateTime.Now.ToString("dd/MM/yyyy")
                });
            }


            if (!string.IsNullOrEmpty(searchValue))
            {
                allProducts = allProducts.Where(p =>
                    p.Email.ToLower().Contains(searchValue) ||
                    p.Role.ToLower().Contains(searchValue) ||
                    p.Status.ToString().Contains(searchValue) ||
                    p.UserName.ToString().Contains(searchValue) ||
                    p.LastLogin.ToLower().Contains(searchValue)
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

        // GET: UserManagementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserManagementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserManagementController/Create
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

        // GET: UserManagementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserManagementController/Edit/5
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

        // GET: UserManagementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserManagementController/Delete/5
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
