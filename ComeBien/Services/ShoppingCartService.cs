using ComeBien.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    internal class ShoppingCartService
    {
        public ShoppingCartService()
        {

        }

        private static ShoppingCartService _instance;
        private ShoppingCart _shoppingCart;

        public static ShoppingCartService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ShoppingCartService();
            }
            return _instance;
        }

        public ShoppingCart ShoppingCart
        {
            get { return _shoppingCart; }
            set
            {
                _shoppingCart = value;
            }
        }

        public void AddProduct(ShoppingCartProduct cartProduct)
        {
            if(ShoppingCart == null)
                ShoppingCart = new ShoppingCart();

            ShoppingCart.Products.Add(cartProduct);
            ShoppingCart.TotalAmount += cartProduct.Price;
        }

    }
}
