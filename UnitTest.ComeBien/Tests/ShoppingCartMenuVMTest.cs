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
    public class ShoppingCartMenuVMTest
    {
        [TestMethod]
        public void ShoppingCartCountCero()
        {
            ShoppingCartMenuVM shoppingCartMenuVM = new ShoppingCartMenuVM();
            ShoppingCartService.GetInstance().ShoppingCart = new ShoppingCart();

            Assert.AreEqual(0, shoppingCartMenuVM.ShoppingCartCount);
        }

        [TestMethod]
        public void ShoppingCartCountMore()
        {
            ShoppingCartMenuVM shoppingCartMenuVM = new ShoppingCartMenuVM();
            ShoppingCartService.GetInstance().ShoppingCart = new ShoppingCart
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

            Assert.AreEqual(3, shoppingCartMenuVM.ShoppingCartCount);
        }
    }
}
