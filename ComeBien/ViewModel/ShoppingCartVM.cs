using ComeBien.DataAccess.Repositories;
using ComeBien.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    internal class ShoppingCartVM : NotificationClass
    {
        private decimal _totalAmount;
        private ObservableCollection<ShoppingCartProductVM> _productsCollection;
        private ICommand _payCommand;
        private ICommand _removeProductCommand;
        private readonly IOrderService _orderService;
        public ShoppingCartVM()
        {
            _orderService = new OrderService();
            InitializeData();
        }

        private void InitializeData()
        {
            IList<ShoppingCartProductVM> shoppingCartProducts = new List<ShoppingCartProductVM>();  
            var shoppingCart = ShoppingCartService.GetInstance().ShoppingCart;
            
            TotalAmount = shoppingCart.TotalAmount;
            int index = 0;
            foreach(var product in shoppingCart.Products)
            {
                shoppingCartProducts.Add(new ShoppingCartProductVM
                {
                    Index = index,
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Ingredients = String.Join(",", product.Ingredients.Select(x => $"{x.Quantity} x {x.Name}"))
                });
                index++;
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

        public ICommand PayCommand
        {
            get
            {
                if (_payCommand == null)
                {
                    SetPayCommand();
                }

                return _payCommand;
            }
        }

        private void SetPayCommand()
        {
            bool canExecute = ShoppingCartService.GetInstance().ShoppingCart.Products.Count > 0;
            _payCommand = new RelayCommand(param => Pay(), canExecute);
        }

        private void Pay()
        {
            _orderService.PayOrder();

            ProductsCollection.Clear();
            TotalAmount = 0;

            MessageBox.Show("Ha comprado con éxito. Vuelva pronto",
                        "Enhorabuena!!!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ICommand RemoveProductCommand
        {
            get
            {
                if (_removeProductCommand == null)
                    _removeProductCommand = new RelayCommand(param => RemoveProduct((int)param), true);

                return _removeProductCommand;
            }
        }

        private void RemoveProduct(int index)
        {
            var product = ProductsCollection.ElementAt(index);
            TotalAmount -= product.Price;
            ProductsCollection.RemoveAt(index);

            ProductsCollection.Where(x => x.Index > index).ToList().ForEach(x => x.Index -= 1);

            ShoppingCartService.GetInstance().RemoveProduct(index);

        }
    }
}
