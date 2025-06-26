namespace Warungku.MVC.Models
{
    public class PosView
    {
        public int Id { get; set; }
        public string Item { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Subtotal { get; set; }
    }
}
