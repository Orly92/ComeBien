using ComeBien.Models.Core;
using ComeBien.ViewModel;
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
            ShoppingCart = new ShoppingCart();
            ShoppingCartMenuVM = new ShoppingCartMenuVM();
        }

        private static ShoppingCartService _instance;
        private ShoppingCart _shoppingCart;
        private ShoppingCartMenuVM _shoppingCartMenuVM;

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

        public ShoppingCartMenuVM ShoppingCartMenuVM
        {
            get { return _shoppingCartMenuVM; }
            set
            {
                _shoppingCartMenuVM = value;
            }
        }

        public void AddProduct(ShoppingCartProduct cartProduct)
        {
            ShoppingCart.Products.Add(cartProduct);
            ShoppingCart.TotalAmount += cartProduct.Price;
            ShoppingCartMenuVM.EditShoppingCart();
        }

        public int CountProduct()
        {
            return ShoppingCart.Products.Count;
        }

    }
}
