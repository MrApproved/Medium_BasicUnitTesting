﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart_V3.Models;
using System.Linq;

namespace ShoppingCart_V3
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
            AssertBasketItemIsAnAppleWithQuantity(Basket.Items.First(), 1);
        }

        [TestMethod]
        public void AddMultipleApplesToOurBasket_BasketItemsShouldContainMultipleApples()
        {
            var quantity = 3;
            for (var i = 0; i < quantity; i++)
                AddAppleToBasket();

            Assert.IsTrue(Basket.Items.Count == 1);
            AssertBasketItemIsAnAppleWithQuantity(Basket.Items.First(), quantity);
        }

        [TestMethod]
        public void AddAnAppleWithAQuantityAmount_BasketItemsShouldContainAnAppleWithTheCorrectQuantity()
        {
            var quantity = 6;
            AddAppleWithQuantityToBasket(quantity);

            Assert.IsTrue(Basket.Items.Count == 1);
            AssertBasketItemIsAnAppleWithQuantity(Basket.Items.First(), quantity);
        }

        [TestMethod]
        public void AddAnAppleAndThenAnAppleWithQuantity_AppleBasketItemHasCorrectQuantity()
        {
            var quantity = 7;
            AddAppleToBasket();
            AddAppleWithQuantityToBasket(quantity);

            Assert.IsTrue(Basket.Items.Count == 1);
            AssertBasketItemIsAnAppleWithQuantity(Basket.Items.First(), 1 + quantity);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-10)]
        public void AddAnAppleWithAnInvalidQuantity_ItemIsNotAddedToBasket(int quantity)
        {
            AddAppleWithQuantityToBasket(quantity);

            Assert.IsTrue(Basket.Items.Count == 0);
        }

        [TestMethod]
        [DataRow(1, 5)]
        [DataRow(1, 0)]
        [DataRow(10, -5)]
        public void ModifyTheQuantityOfAnExistingBasketItem_BasketItemQuantityReflectsTheChange(int initialQuantity, int modifyQuantity)
        {
            AddAppleWithQuantityToBasket(initialQuantity);
            Basket.ModifyQuantity(Apple.ReferenceCode, modifyQuantity);

            Assert.IsTrue(Basket.Items.Count == 1);
            AssertBasketItemIsAnAppleWithQuantity(Basket.Items.First(), initialQuantity + modifyQuantity);
        }

        [TestMethod]
        [DataRow(15, -15)]
        [DataRow(1, -100)]
        public void ModifyTheQuantityOfAnExistingItemToZeroOrBelowZero_BasketItemIsRemoved(int initialQuantity, int modifyQuantity)
        {
            AddAppleWithQuantityToBasket(initialQuantity);
            Basket.ModifyQuantity(Apple.ReferenceCode, modifyQuantity);

            Assert.IsTrue(Basket.Items.Count == 0);
        }

        [TestMethod]
        public void ModifyTheQuantityOfAnItemThatDoesNotExistInBasket_NothingHappens()
        {
            Basket.ModifyQuantity("ReferenceCode", int.MaxValue);

            Assert.IsTrue(Basket.Items.Count == 0);
        }

        [TestMethod]
        public void RemoveAnEntireItemFromBasket_BasketIsNowEmpty()
        {
            AddAppleWithQuantityToBasket(int.MaxValue);
            Basket.RemoveEntireItem(Apple.ReferenceCode);

            Assert.IsTrue(Basket.Items.Count == 0);
        }

        private void AddAppleToBasket()
            => AddAppleWithQuantityToBasket(1);

        private void AddAppleWithQuantityToBasket(int quantity)
            => Basket.AddItem(Apple.ReferenceCode, Apple.Name, Apple.Price, quantity);

        private void AssertBasketItemIsAnAppleWithQuantity(BasketItem item, int quantity)
        {
            Assert.AreEqual(item.Name, Apple.Name);
            Assert.AreEqual(item.Price, Apple.Price);
            Assert.AreEqual(item.ReferenceCode, Apple.ReferenceCode);
            Assert.AreEqual(item.Quantity, quantity);
        }
    }
}