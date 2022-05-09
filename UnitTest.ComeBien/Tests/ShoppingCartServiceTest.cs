using ComeBien.Models.Core;
using ComeBien.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ComeBien.Tests
{
    [TestClass]
    public class ShoppingCartServiceTest
    {
        [TestMethod]
        public void GetInstance()
        {
            var service = ShoppingCartService.GetInstance();

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void Reset()
        {
            ShoppingCartService.GetInstance().ShoppingCart = new ShoppingCart
            {
                TotalAmount = 10,
                Products = new List<ShoppingCartProduct>()
                {
                    new ShoppingCartProduct
                    {
                        Price = 10,
                        ProductId = 1,
                        Name = "prueba"
                    }
                }
            };

            ShoppingCartService.GetInstance().Reset();

            Assert.AreEqual(0, ShoppingCartService.GetInstance().ShoppingCart.TotalAmount);
            Assert.AreEqual(0, ShoppingCartService.GetInstance().ShoppingCart.Products.Count);
        }

        [TestMethod]
        public void CountProduct()
        {
            ShoppingCartService.GetInstance().ShoppingCart = new ShoppingCart
            {
                TotalAmount = 30,
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
                        Price = 10,
                        ProductId = 3,
                        Name = "prueba"
                    }
                }
            };

            int countProduct = ShoppingCartService.GetInstance().CountProduct();

            Assert.AreEqual(3, countProduct);
        }

        [TestMethod]
        public void RemoveAllProduct()
        {
            ShoppingCartService.GetInstance().ShoppingCart = new ShoppingCart
            {
                TotalAmount = 30,
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
                        Price = 10,
                        ProductId = 3,
                        Name = "prueba"
                    }
                }
            };

            ShoppingCartService.GetInstance().RemoveProduct(0);
            ShoppingCartService.GetInstance().RemoveProduct(0);
            ShoppingCartService.GetInstance().RemoveProduct(0);

            Assert.AreEqual(0, ShoppingCartService.GetInstance().ShoppingCart.TotalAmount);
            Assert.AreEqual(0, ShoppingCartService.GetInstance().ShoppingCart.Products.Count);
        }

        [TestMethod]
        public void RemoveOneProduct()
        {
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

            ShoppingCartService.GetInstance().RemoveProduct(1);

            Assert.AreEqual(18, ShoppingCartService.GetInstance().ShoppingCart.TotalAmount);
            Assert.AreEqual(2, ShoppingCartService.GetInstance().ShoppingCart.Products.Count);
        }

        [TestMethod]
        public void AddProduct()
        {
            ShoppingCartService.GetInstance().ShoppingCart = new ShoppingCart
            {
                TotalAmount = 10,
                Products = new List<ShoppingCartProduct>()
                {
                    new ShoppingCartProduct
                    {
                        Price = 10,
                        ProductId = 1,
                        Name = "prueba"
                    },
                }
            };

            ShoppingCartService.GetInstance().AddProduct(new ShoppingCartProduct
            {
                Name = "prueba 1",
                Price = 23,
                ProductId = 2
            });

            Assert.AreEqual(33, ShoppingCartService.GetInstance().ShoppingCart.TotalAmount);
            Assert.AreEqual(2, ShoppingCartService.GetInstance().ShoppingCart.Products.Count);

            ShoppingCartService.GetInstance().AddProduct(new ShoppingCartProduct
            {
                Name = "prueba 2",
                Price = 12,
                ProductId = 3
            });

            Assert.AreEqual(45, ShoppingCartService.GetInstance().ShoppingCart.TotalAmount);
            Assert.AreEqual(3, ShoppingCartService.GetInstance().ShoppingCart.Products.Count);
        }
    }
}
