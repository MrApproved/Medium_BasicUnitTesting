using ShoppingCart_V1.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart_V1
{
    public class Basket
    {
        public IReadOnlyCollection<BasketItem> Items
            => _items.ToList().AsReadOnly();

        private readonly ICollection<BasketItem> _items;

        public Basket()
        {
            _items = new List<BasketItem>();
        }

        public void AddItem(string referenceCode, string name, decimal price)
        {
            _items.Add(new BasketItem()
            {
                ReferenceCode = referenceCode,
                Name = name,
                Price = price
            });
        }

        public void RemoveItem(string referenceCode)
        {
            _items.Remove(Items.FirstOrDefault(x => x.ReferenceCode == referenceCode));
        }
    }
}
