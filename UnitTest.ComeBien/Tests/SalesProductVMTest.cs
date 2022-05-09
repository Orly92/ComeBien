using ComeBien.DataAccess;
using ComeBien.Services;
using ComeBien.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ComeBien.Tests
{
    [TestClass]
    public class SalesProductVMTest
    {
        [TestMethod]
        public async Task InitializeCollection()
        {
            SalesProductVM salesProductVM = new SalesProductVM();
            int totalIngredients = 0;
            int totalProducts = 0;

            using (var dbContext = new ComeBienContext())
            {
                totalIngredients = await dbContext.Ingredients.CountAsync();
                totalProducts = await dbContext.Products.CountAsync();
            }

            Assert.AreEqual(totalProducts, salesProductVM.ProductsCollection.Count);
            Assert.AreEqual(totalIngredients, salesProductVM.IngredCollection.Count);
        }

        [TestMethod]
        public void AddProductToShoppingCartWithoutIngredient()
        {
            int idProduct = 1;
            ShoppingCartService.GetInstance().Reset();
            SalesProductVM salesProductVM = new SalesProductVM();
            var product = salesProductVM.ProductsCollection.FirstOrDefault(x => x.Id == idProduct);

            salesProductVM.AddProductCommand.Execute(idProduct);

            Assert.AreEqual(product.Price, ShoppingCartService.GetInstance().ShoppingCart.TotalAmount);
            Assert.AreEqual(1, ShoppingCartService.GetInstance().ShoppingCart.Products.Count);
            Assert.IsTrue(ShoppingCartService.GetInstance().ShoppingCart.Products.Any(x=>x.ProductId == idProduct));
        }

        [TestMethod]
        public void AddProductToShoppingCartWithIngredient()
        {
            int idProduct = 1;
            ShoppingCartService.GetInstance().Reset();
            SalesProductVM salesProductVM = new SalesProductVM();
            var product = salesProductVM.ProductsCollection.FirstOrDefault(x => x.Id == idProduct);
            salesProductVM.IngredCollection.ToList().ForEach(x => x.Quantity = 1);
            decimal sumIngredient = salesProductVM.IngredCollection.Sum(x => x.Quantity * x.Price);

            salesProductVM.AddProductCommand.Execute(idProduct);

            Assert.AreEqual(product.Price + sumIngredient, ShoppingCartService.GetInstance().ShoppingCart.TotalAmount);
            Assert.AreEqual(1, ShoppingCartService.GetInstance().ShoppingCart.Products.Count);
            Assert.IsTrue(ShoppingCartService.GetInstance().ShoppingCart.Products.Any(x => x.ProductId == idProduct));
            Assert.IsFalse(ShoppingCartService.GetInstance().ShoppingCart.Products.FirstOrDefault().Ingredients.Any(x=>x.Quantity != 1));
        }
    }
}
