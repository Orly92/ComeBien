using ComeBien.DataAccess;
using ComeBien.DataAccess.Repositories;
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
    public class ProductsRepositoryTests
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsRepositoryTests()
        {
            _productsRepository = new ProductsRepository();
        }

        [TestMethod]
        public async Task GetAllCountedTest()
        {
            int totalProducts = 2;

            using (var dbContext = new ComeBienContext())
            {
                totalProducts = await dbContext.Products.CountAsync();
            }

            var products = await _productsRepository.GetAll();

            Assert.AreEqual(totalProducts, products.Count);
        }
    }
}
