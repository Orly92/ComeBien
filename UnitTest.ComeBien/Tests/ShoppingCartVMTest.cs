using ComeBien.Models.Core;
using ComeBien.Services;
using ComeBien.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ComeBien.Tests
{
    [TestClass]
    public class ShoppingCartVMTest
    {
        [TestMethod]
        public void InitializeData()
        {
            ShoppingCart cart = new ShoppingCart
            {
                TotalAmount = 28,
                Products = new List<ShoppingCartProduct>()
                {
                    new ShoppingCartProduct
                    {
                        Price = 10,
                        ProductId = 1,
                        Name = "prueba"
                    },
                    new ShoppingCartProduct
                    {
                        Price = 10,
                        ProductId = 2,
                        Name = "prueba"
                    },
                    new ShoppingCartProduct
                    {
                        Price = 8,
                        ProductId = 3,
                        Name = "prueba"
                    }
                }
            };
            ShoppingCartService.GetInstance().ShoppingCart = cart;

            ShoppingCartVM shoppingCartVM = new ShoppingCartVM();

            Assert.AreEqual(cart.TotalAmount, shoppingCartVM.TotalAmount);
            Assert.AreEqual(cart.Products.Count, shoppingCartVM.ProductsCollection.Count);
        }

        [TestMethod]
        public void RemoveAllProduct()
        {
            ShoppingCart cart = new ShoppingCart
            {
                TotalAmount = 28,
                Products = new List<ShoppingCartProduct>()
                {
                    new ShoppingCartProduct
                    {
                        Price = 10,
                        ProductId = 1,
                        Name = "prueba"
                    },
                    new ShoppingCartProduct
                    {
                        Price = 10,
                        ProductId = 2,
                        Name = "prueba"
                    },
                    new ShoppingCartProduct
                    {
                        Price = 8,
                        ProductId = 3,
                        Name = "prueba"
                    }
                }
            };
            ShoppingCartService.GetInstance().ShoppingCart = cart;

            ShoppingCartVM shoppingCartVM = new ShoppingCartVM();

            shoppingCartVM.RemoveProductCommand.Execute(2);
            shoppingCartVM.RemoveProductCommand.Execute(1);
            shoppingCartVM.RemoveProductCommand.Execute(0);

            Assert.AreEqual(0, shoppingCartVM.TotalAmount);
            Assert.AreEqual(0, shoppingCartVM.ProductsCollection.Count);
        }

        [TestMethod]
        public void Remove2Product()
        {
            ShoppingCart cart = new ShoppingCart
            {
                TotalAmount = 28,
                Products = new List<ShoppingCartProduct>()
                {
                    new ShoppingCartProduct
                    {
                        Price = 10,
                        ProductId = 1,
                        Name = "prueba"
                    },
                    new ShoppingCartProduct
                    {
                        Price = 10,
                        ProductId = 2,
                        Name = "prueba"
                    },
                    new ShoppingCartProduct
                    {
                        Price = 8,
                        ProductId = 3,
                        Name = "prueba"
                    }
                }
            };
            ShoppingCartService.GetInstance().ShoppingCart = cart;

            ShoppingCartVM shoppingCartVM = new ShoppingCartVM();

            shoppingCartVM.RemoveProductCommand.Execute(0);
            shoppingCartVM.RemoveProductCommand.Execute(1);

            Assert.AreEqual(8, shoppingCartVM.TotalAmount);
            Assert.AreEqual(1, shoppingCartVM.ProductsCollection.Count);
        }
    }
}
