using ShoppingCart_V4.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart_V4
{
    public class Basket
    {
        public IReadOnlyCollection<BasketItem> Items
            => _items.ToList().AsReadOnly();

        private ICollection<BasketItem> _items;

        public Basket() => Empty();

        public void AddItem(string referenceCode, string name, decimal price, int quantity = 1)
        {
            if (quantity <= 0)
                return;

            var item = _items.FirstOrDefault(x => x.ReferenceCode == referenceCode);

            if (item != null)
                item.Quantity += quantity;
            else
            {
                _items.Add(new BasketItem()
                {
                    ReferenceCode = referenceCode,
                    Name = name,
                    Price = price,
                    Quantity = quantity
                });
            }
        }

        public void ModifyQuantity(string referenceCode, int value)
        {
            var item = _items.FirstOrDefault(x => x.ReferenceCode == referenceCode);

            if (item != null)
            {
                item.Quantity += value;

                if (item.Quantity <= 0)
                    RemoveEntireItem(referenceCode);
            }
        }

        public void RemoveEntireItem(string referenceCode)
            => _items.Remove(Items.FirstOrDefault(x => x.ReferenceCode == referenceCode));

        public void Empty()
            => _items = new List<BasketItem>();
    }
}
