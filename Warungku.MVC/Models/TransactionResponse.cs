namespace Warungku.MVC.Models
{
    public class TransactionResponse
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string User  { get; set; }

        public decimal Total { get; set; }

        public int Discount { get; set; }

        public int Voucher { get; set; }

        public decimal GrandTotal { get; set; }
    }
}
