using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Warungku.MVC.Models
{
    public class UserRequest
    {
        [Required]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "The Role Field is Required")]
        public int? RoleId { get; set; }
        public List<SelectListItem> Roles { get; set; }
        
        [Required(ErrorMessage = "The Status Field is Required")]
        public int? StatusId { get; set; }
        public List<SelectListItem> Statuses { get; set; }
    }
}
