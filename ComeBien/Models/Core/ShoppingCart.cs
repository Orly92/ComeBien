using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Models.Core
{
    internal class ShoppingCart
    {
        public ShoppingCart()
        {
            Products = new List<ShoppingCartProduct>();
        }
        public decimal TotalAmount { get; set; }

        public IList<ShoppingCartProduct> Products { get; set; }
    }

    public class ShoppingCartProduct
    {
        public ShoppingCartProduct()
        {
            Ingredients = new List<ShoppingCartIngredients>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IList<ShoppingCartIngredients> Ingredients { get; set; }
    }

    public class ShoppingCartIngredients
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
