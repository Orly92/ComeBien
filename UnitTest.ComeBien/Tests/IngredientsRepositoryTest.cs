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
    public class IngredientsRepositoryTest
    {
        private readonly IIngredientsRepository _ingredientsRepository;

        public IngredientsRepositoryTest()
        {
            _ingredientsRepository = new IngredientsRepository();
        }

        [TestMethod]
        public async Task GetAllCountedTest()
        {
            int totalIngredients = 0;

            using (var dbContext = new ComeBienContext())
            {
                totalIngredients = await dbContext.Ingredients.CountAsync();
            }

            var ingredients = await _ingredientsRepository.GetAll();

            Assert.AreEqual(totalIngredients, ingredients.Count);
        }
    }
}
