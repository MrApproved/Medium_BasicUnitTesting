using ShoppingCart_V2.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart_V2
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
            var item = _items.FirstOrDefault(x => x.ReferenceCode == referenceCode);

            if (item != null)
                item.Quantity += 1;
            else
            {
                _items.Add(new BasketItem()
                {
                    ReferenceCode = referenceCode,
                    Name = name,
                    Price = price,
                    Quantity = 1
                });
            }
        }

        public void RemoveItem(string referenceCode)
        {
            var item = _items.FirstOrDefault(x => x.ReferenceCode == referenceCode);

            if (item != null)
            {
                item.Quantity -= 1;

                if(item.Quantity <= 0)
                    _items.Remove(Items.FirstOrDefault(x => x.ReferenceCode == referenceCode));
            }
        }
    }
}
