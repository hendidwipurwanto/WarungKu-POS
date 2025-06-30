using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warungku.Core.Domain.DTOs
{
    public class PosRequest
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public List<SelectListItem>? Products { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Subtotal { get; set; }
    }

    public class PosResponse
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string Item { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Subtotal { get; set; }
    }
}
