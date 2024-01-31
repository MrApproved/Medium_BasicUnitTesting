namespace ShoppingCart_V4.Models
{
    public class BasketItem
    {
        public string ReferenceCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}