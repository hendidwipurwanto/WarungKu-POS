using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warungku.Core.Domain.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string User { get; set; }

        public decimal Total { get; set; }

        public int Discount { get; set; }

        public int Voucher { get; set; }

        public decimal GrandTotal { get; set; }
    }
}
