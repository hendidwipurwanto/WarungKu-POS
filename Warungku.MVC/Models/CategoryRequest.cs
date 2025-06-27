using System.ComponentModel.DataAnnotations;

namespace Warungku.MVC.Models
{
    public class CategoryRequest
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

    }
}
