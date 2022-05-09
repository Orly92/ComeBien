using ComeBien.DataAccess;
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
    public class CRUDIngredientsVMTest
    {
        [TestMethod]
        public async Task InitializeCollection()
        {
            CRUDIngredientsVM cRUDIngredientsVM = new CRUDIngredientsVM();
            int totalIngredients = 0;

            using (var dbContext = new ComeBienContext())
            {
                totalIngredients = await dbContext.Ingredients.CountAsync();
            }

            Assert.AreEqual(totalIngredients, cRUDIngredientsVM.IngredientsCollection.Count);
        }

        [TestMethod]
        public void ResetCommand()
        {
            CRUDIngredientsVM cRUDIngredientsVM = new CRUDIngredientsVM();
            cRUDIngredientsVM.SelectedIngredient.EsName = "pruebaasas";
            cRUDIngredientsVM.SelectedIngredient.FrName = "grfssdsds";
            cRUDIngredientsVM.SelectedIngredient.Price = (decimal)256.00;

            cRUDIngredientsVM.ResetCommand.Execute(null);

            Assert.AreEqual(null, cRUDIngredientsVM.SelectedIngredient.EsName);
            Assert.AreEqual(null, cRUDIngredientsVM.SelectedIngredient.FrName);
            Assert.AreEqual(0, cRUDIngredientsVM.SelectedIngredient.Price);
        }

        [TestMethod]
        public async Task SelectCommand()
        {
            CRUDIngredientsVM cRUDIngredientsVM = new CRUDIngredientsVM();

            int total = cRUDIngredientsVM.IngredientsCollection.Count;
            for(int i = 0; i < total; i++)
            {
                var ingredient = cRUDIngredientsVM.IngredientsCollection.ElementAt(i);
                await cRUDIngredientsVM.Select(ingredient.Id);

                Assert.AreEqual(ingredient.Id, cRUDIngredientsVM.SelectedIngredient.Id);
                Assert.AreEqual(ingredient.EsName, cRUDIngredientsVM.SelectedIngredient.EsName);
                Assert.AreEqual(ingredient.Price, cRUDIngredientsVM.SelectedIngredient.Price);
                Assert.AreEqual(ingredient.EnName, cRUDIngredientsVM.SelectedIngredient.EnName);
                Assert.AreEqual(ingredient.FrName, cRUDIngredientsVM.SelectedIngredient.FrName);
            }
        }
    }
}
