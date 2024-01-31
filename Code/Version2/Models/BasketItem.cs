namespace ShoppingCart_V2.Models
{
    public class BasketItem
    {
        public string ReferenceCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}