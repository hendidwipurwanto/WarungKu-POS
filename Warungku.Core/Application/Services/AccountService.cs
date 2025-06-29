using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Warungku.Core.Application.Interfaces;
using Warungku.Core.Domain.DTOs;
using Warungku.Core.Domain.Entities;
using Warungku.Core.Infrastructure.Data;

namespace Warungku.Core.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AccountService(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
        {
            await EnsureRolesExist();

            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.FullName,
                Address = request.Address,
                 EmailConfirmed=true,
                  RoleId=request.RoleId == null ? 3 : request.RoleId,
                   StatusId= request.StatusId == null ? 3 : request.StatusId
            };
            try
            {
                
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    if (request.StatusId == null && request.RoleId == null)
                    {
                        await _userManager.AddToRoleAsync(user, "Staff"); // default role after register, it can be changed through user management page
                    }
                    else
                    {
                        string roleName = GetRoleName(request.RoleId);

                        await _userManager.AddToRoleAsync(user, roleName);
                    }                       
                }

                return result;
            }
            catch (Exception ex)
            {
                var users = await _context.Users.ToListAsync();
                throw ex;
            }
           
        }

        public async Task<SignInResult> LoginAsync(LoginRequest request)
        {
            var currentUser = new ApplicationUser();
            if (IsEmail(request.Username)) 
            {
                currentUser = await _userManager.FindByEmailAsync(request.Username);
            }
            else
            {
                currentUser = await _userManager.FindByNameAsync(request.Username);
            }
            var userId = currentUser?.Id;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.LastLogin = DateTime.Now;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();


            return await _signInManager.PasswordSignInAsync(currentUser.UserName,request.Password, request.RememberMe, false);
        }

        private bool IsEmail(string input)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(input);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task EnsureRolesExist()
        {
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var response = new List<UserResponse>();
            var applicationUser = await _context.Users.ToListAsync();

        
            foreach (var item in applicationUser)
            {
                var roles = await _userManager.GetRolesAsync(item);
                string role = roles.FirstOrDefault();
                var temp = new UserResponse() { Id= item.Id, Email=item.Email, UserName = item.UserName, StatusName = GetStatusName(item.StatusId), RoleName=GetRoleName(item.RoleId), RoleId=item.RoleId, LastLogin= item.LastLogin == null? null:item.LastLogin.Value.ToString("MM/dd/yyyy hh:mm:ss tt") };
                response.Add(temp);
            }



            return response;
        }
        private string GetStatusName(int? statusId)
        {
            if(statusId ==1)
            {
                return "Active";
            }else if(statusId == 2)
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
        public async Task<UserRequest> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == id);
            var response = new UserRequest() { Email = user.Email, UserName = user.UserName, StatusId=user.StatusId, RoleId=user.RoleId };

            return response;
         }

        public async Task<bool> UpdateUserAsync(UserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if(user != null)
            {
                if(request.RoleId != null)
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    string roleName = GetRoleName(request.RoleId);
                    var addResult = await _userManager.AddToRoleAsync(user, roleName);
                }            
              

                user.Email = request.Email; 
                user.UserName = request.UserName;
                user.StatusId = request.StatusId;
                user.RoleId = request.RoleId;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> DeleteUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }



            return false;
        }
    }

}
