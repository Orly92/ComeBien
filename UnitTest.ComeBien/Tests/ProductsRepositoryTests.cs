using ComeBien.DataAccess.Repositories;
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

            var products = await _productsRepository.GetAll();

            Assert.AreEqual(products.Count, totalProducts);
        }
    }
}
