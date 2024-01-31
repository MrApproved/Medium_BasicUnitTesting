using ShoppingCart_V5.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart_V5
{
    public class Basket
    {
        public IReadOnlyCollection<BasketItem> Items
            => _items.Select(x => x.Value).ToList().AsReadOnly();

        private IDictionary<string, BasketItem> _items;

        public Basket() => Empty();

        public void AddItem(string referenceCode, string name, decimal price, int quantity = 1)
        {
            if (quantity <= 0)
                return;

            if (_items.TryGetValue(referenceCode, out var item))
                ModifyQuantity(item, quantity);
            else
                AddNewBasketItem();

            void AddNewBasketItem()
            {
                _items[referenceCode] = new BasketItem()
                {
                    ReferenceCode = referenceCode,
                    Name = name,
                    Price = price,
                    Quantity = quantity
                };
            }
        }

        public void ModifyQuantity(string referenceCode, int value)
        {
            if (_items.TryGetValue(referenceCode, out var item))
                ModifyQuantity(item, value);
        }

        private void ModifyQuantity(BasketItem item, int value)
        {
            item.Quantity += value;

            if (item.Quantity <= 0)
                RemoveEntireItem(item.ReferenceCode);
        }

        public void RemoveEntireItem(string referenceCode)
            => _items.Remove(referenceCode);

        public void Empty()
            => _items = new Dictionary<string, BasketItem>();
    }
}
