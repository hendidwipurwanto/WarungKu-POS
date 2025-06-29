using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warungku.Core.Domain.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterRequest
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string? Address { get; set; }

        public int? StatusId { get; set; }
        public int? RoleId { get; set; }
    }

    public class UserRequest
    {
        public string? Id { get; set; }

        [Required]
        public string UserName { get; set; }

       
        public string? Password { get; set; }

        [Required(ErrorMessage = "The Role Field is Required")]
        public int? RoleId { get; set; }
        public List<SelectListItem>? RolesOptions { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Status Field is Required")]
        public int? StatusId { get; set; }
        public List<SelectListItem>? StatusesOptions { get; set; }
    }

    public class AuthResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }

    public class UserResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public List<SelectListItem> RoleOptions { get; set; }

        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public List<SelectListItem> StatusOptions { get; set; }

        public string LastLogin { get; set; }
    }
}
