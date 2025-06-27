using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warungku.MVC.Models
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
