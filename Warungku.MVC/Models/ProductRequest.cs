using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Warungku.MVC.Models
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Category Field is Required")]
        public int? CategoryId { get; set; } 
        
        public List<SelectListItem> Categories { get; set; }
    
        [Required]
        public string Price { get; set; }

        [Required]
        public string Stock { get; set; } 

    }
}
