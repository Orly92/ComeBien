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
    public class IngredientVMTest
    {
        public IngredientVMTest()
        {

        }

        [TestMethod]
        public void ResetTest()
        {
            IngredientVM ingredient = new IngredientVM
            {
                Id = 1,
                Name = "Prueba",
                Price = 1,
                Quantity = 100
            };

            ingredient.ResetQuantity();

            Assert.AreEqual(0, ingredient.Quantity);
        }

        [TestMethod]
        [DataRow(2, 1)]
        [DataRow(0, 12)]
        [DataRow(123, 3)]
        [DataRow(2, 6)]
        public void AddIngTest(int quantityInitial,int executeCount)
        {
            int total = quantityInitial + executeCount;
            IngredientVM ingredient = new IngredientVM
            {
                Id = 1,
                Name = "Prueba",
                Price = 1,
                Quantity = quantityInitial
            };

            for(int i=0;i< executeCount; i++)
            {
                ingredient.AddCommand.Execute(ingredient);
            }
            
            Assert.AreEqual(total, ingredient.Quantity);
        }

        [TestMethod]
        [DataRow(0, 34,0)]
        [DataRow(2, 2,0)]
        [DataRow(10, 3,7)]
        public void RemIngTest(int quantityInitial, int executeCount,int expected)
        {
            IngredientVM ingredient = new IngredientVM
            {
                Id = 1,
                Name = "Prueba",
                Price = 1,
                Quantity = quantityInitial
            };

            for (int i = 0; i < executeCount; i++)
            {
                ingredient.RemoveCommand.Execute(ingredient);
            }
            
            Assert.AreEqual(expected, ingredient.Quantity);
        }
    }
}
