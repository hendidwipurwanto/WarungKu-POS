using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warungku.MVC.Models
{
    public class UserResponse_
    {
        public int Id { get; set; }

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
