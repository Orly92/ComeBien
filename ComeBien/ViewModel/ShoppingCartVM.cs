using ComeBien.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.ViewModel
{
    internal class ShoppingCartVM : NotificationClass
    {
        private decimal _totalAmount;
        private ObservableCollection<ShoppingCartProductVM> _productsCollection;
        public ShoppingCartVM()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            IList<ShoppingCartProductVM> shoppingCartProducts = new List<ShoppingCartProductVM>();  
            var shoppingCart = ShoppingCartService.GetInstance().ShoppingCart;
            
            TotalAmount = shoppingCart.TotalAmount;
            foreach(var product in shoppingCart.Products)
            {
                string ingredients = "";
                foreach(var ingredient in product.Ingredients)
                {
                    ingredients += $"{ingredient.Quantity} x {ingredient.Name}";
                }
                shoppingCartProducts.Add(new ShoppingCartProductVM
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Ingredients = ingredients
                });
            }
            ProductsCollection = new ObservableCollection<ShoppingCartProductVM>(shoppingCartProducts);
        }

        public decimal TotalAmount { 
            get { return _totalAmount; } 
            set { 
                _totalAmount = value;
                OnPropertyChanged("TotalAmount");
            } 
        }
        public ObservableCollection<ShoppingCartProductVM> ProductsCollection
        {
            get { return _productsCollection; }
            set
            {
                _productsCollection = value;
                OnPropertyChanged("ProductsCollection");
            }
        }
    }
}
