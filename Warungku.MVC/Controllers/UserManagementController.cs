using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Drawing.Text;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Application.Services;
using Warungku.Core.Domain.DTOs;

namespace Warungku.MVC.Controllers
{
    public class UserManagementController : Controller
    {
        private IAccountService _accountService;
        public UserManagementController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        public async Task<ActionResult> Index()
        {
            ViewBag.currentUser = User.Identity.Name;
            var user = await _accountService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetUsers()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var allUsers = await _accountService.GetAllAsync();
            var totalRecordBeforeFiltered = allUsers.Count();


            if (!string.IsNullOrEmpty(searchValue))
            {
                allUsers = allUsers.Where(p =>
                    p.Email.ToLower().Contains(searchValue) ||
                    p.RoleName.ToLower().Contains(searchValue) ||
                    p.StatusName.ToLower().Contains(searchValue) ||
                    p.UserName.ToString().Contains(searchValue) ||
                    p.LastLogin.ToLower().Contains(searchValue)
                ).ToList();
            }

            int totalRecords = allUsers.Count();
            var data = allUsers.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = totalRecordBeforeFiltered,
                recordsFiltered = totalRecords, 
                data = data
            });
        }

       

        [HttpGet]
        public ActionResult Create()
        {
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Manager" },
                new SelectListItem { Value = "2", Text = "Admin" },
                new SelectListItem { Value="3", Text="Staff" }
            };
            var statuses = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Active" },
                new SelectListItem { Value = "2", Text = "InActive" },
                new SelectListItem { Value="3", Text="Draft" }
            };

            var model = new UserRequest();
            model.RolesOptions = new List<SelectListItem>();
            model.StatusesOptions = new List<SelectListItem>();
            model.RolesOptions.AddRange(roles);
            model.StatusesOptions.AddRange(statuses);

            return PartialView("_addModal", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserRequest request)
        {
            if (ModelState.IsValid)
            { var newUser = new RegisterRequest() { Username = request.UserName, Email=request.Email, 
                Password=request.Password, RoleId=request.RoleId, StatusId=request.StatusId  };
                var result = await _accountService.RegisterAsync(newUser);

                return Json(new { success = true, message = "Data Saved Successfully!" });
            }


            return PartialView("_addModal", request);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Manager" },
                new SelectListItem { Value = "2", Text = "Admin" },
                new SelectListItem { Value="3", Text="Staff" }
            };
            var statuses = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Active" },
                new SelectListItem { Value = "2", Text = "InActive" },
                new SelectListItem { Value="3", Text="Draft" }
            };

            var model = await _accountService.GetUserById(id);

            model.Id = id;
            model.RolesOptions = new List<SelectListItem>();
            model.StatusesOptions = new List<SelectListItem>();
            model.RolesOptions.AddRange(roles);
            model.StatusesOptions.AddRange(statuses);

            return PartialView("_editModal", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string  id, UserRequest request)
        {
            if (ModelState.IsValid)
            {
                var res = await _accountService.UpdateUserAsync(request);
                return Json(new { success = true, message = "Data Saved Successfully!" });
            }
           



            return PartialView("_editModal", request);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {

            var result = await _accountService.DeleteUserByIdAsync(id);

           

            return Json(new { success = result, message = result == true ? "Data Deleted Successfully!": "Data Deleted Un-Successfully!" });

        }


        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            var response = await _accountService.GetUserById(id);

            return PartialView("_detailModal", new UserResponse()
            {
                Email = response.Email,
                UserName = response.UserName,
                RoleName = GetRoleName(response.RoleId),
                StatusName = GetStatusName(response.StatusId)
            });
        }

        private string GetStatusName(int? statusId)
        {
            if (statusId == 1)
            {
                return "Active";
            }
            else if (statusId == 2)
            {
                return "InActive";
            }
            else
            {
                return "Draft";
            }

        }
        private string GetRoleName(int? roleId)
        {
            if (roleId == 1)
            {
                return "Manager";
            }
            else if (roleId == 2)
            {
                return "Admin";
            }
            else
            {
                return "Staff";
            }

        }
    }
}
