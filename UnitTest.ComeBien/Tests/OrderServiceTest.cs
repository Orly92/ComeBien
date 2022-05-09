using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Core;
using ComeBien.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ComeBien.Tests
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestMethod]
        public void PayOrderFineReset()
        {
            var orderRepositoryMock = new Mock<OrderRepository>().As<IOrderRepository>();
            orderRepositoryMock.Setup(x => x.CreateOrder(It.IsAny<ShoppingCart>()))
                .ReturnsAsync(2);

            IOrderService orderService = new OrderService(orderRepositoryMock.Object);

            ShoppingCart cart = new ShoppingCart
            {
                TotalAmount=20,
                Products = new List<ShoppingCartProduct>()
                {
                    new ShoppingCartProduct
                    {
                        Name="prueba1",
                        Price=10,
                        ProductId=1
                    },new ShoppingCartProduct
                    {
                        Name="prueba2",
                        Price=10,
                        ProductId=2
                    }
                }
            };

            ShoppingCartService.GetInstance().ShoppingCart = cart;

            orderService.PayOrder();

            //Se debio resetear correctamente los datos
            Assert.AreEqual(0, ShoppingCartService.GetInstance().ShoppingCart.TotalAmount);
            Assert.AreEqual(0, ShoppingCartService.GetInstance().ShoppingCart.Products.Count);
        }
    }
}
