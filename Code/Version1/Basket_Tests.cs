﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart_V1.Models;
using System.Linq;

namespace ShoppingCart_V1
{
    [TestClass]
    public class Basket_Tests
    {
        private static class Apple
        {
            public static string ReferenceCode = "AA000";
            public static string Name = "Apple";
            public static decimal Price = 0.70m;
        }

        private Basket Basket { get; set; }

        [TestInitialize]
        public void Init()
        {
            Basket = new Basket();
        }

        [TestMethod]
        public void WhenABasketIsConstructed_ItShouldBeEmpty()
        {
            Assert.IsTrue(Basket.Items.Count == 0);
        }

        [TestMethod]
        public void AddOneAppleToOurBasket_BasketItemsShouldContainOneApple()
        {
            AddAppleToBasket();

            Assert.IsTrue(Basket.Items.Count == 1);
            AssertBasketItemIsAnApple(Basket.Items.First());
        }

        [TestMethod]
        public void AddMultipleApplesToOurBasket_BasketItemsShouldContainMultipleApples()
        {
            var quantity = 3;
            for(var i = 0; i < quantity; i++)
                AddAppleToBasket();

            for (var i = 0; i < quantity; i++)
                AssertBasketItemIsAnApple(Basket.Items.ElementAt(i));

            Assert.IsTrue(Basket.Items.Count == 3);
        }

        [TestMethod]
        public void RemoveAnItemThatIsNotInOurBasket_NoExceptionShouldBeThrown()
        {
            Basket.RemoveItem("ReferenceCode");

            Assert.IsTrue(Basket.Items.Count == 0);
        }

        [TestMethod]
        public void RemoveAnItemThatIsNotInOurBasket_ItemsInBasketShouldNotBeModified()
        {
            AddAppleToBasket();
            Basket.RemoveItem("ReferenceCode");

            Assert.IsTrue(Basket.Items.Count == 1);
        }

        [TestMethod]
        public void RemoveAnItemThatIsInOurBasket_ItemIsRemovedAndOurBasketShouldBeEmpty()
        {
            AddAppleToBasket();
            RemoveAppleFromBasket();

            Assert.IsTrue(Basket.Items.Count == 0);
        }

        [TestMethod]
        public void RemoveOneItemFromOutBasket_BasketHasItemRemovedButItemsStillRemain()
        {
            for(var i = 0; i < 2; i++)
                AddAppleToBasket();
            RemoveAppleFromBasket();

            Assert.IsTrue(Basket.Items.Count == 1);
            AssertBasketItemIsAnApple(Basket.Items.First());
        }

        [TestMethod]
        public void RemoveAllItemsFromBasket_BasketShouldBeEmpty()
        {
            var quantity = 10;
            for (var i = 0; i < quantity; i++)
                AddAppleToBasket();

            foreach (var item in Basket.Items)
                RemoveAppleFromBasket();

            Assert.IsTrue(Basket.Items.Count == 0);
        }

        private void AddAppleToBasket()
            => Basket.AddItem(Apple.ReferenceCode, Apple.Name, Apple.Price);

        private void RemoveAppleFromBasket()
            => Basket.RemoveItem(Apple.ReferenceCode);

        private void AssertBasketItemIsAnApple(BasketItem item)
        {
            Assert.AreEqual(item.Name, Apple.Name);
            Assert.AreEqual(item.Price, Apple.Price);
            Assert.AreEqual(item.ReferenceCode, Apple.ReferenceCode);
        }
    }
}