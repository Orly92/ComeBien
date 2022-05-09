using ComeBien.DataAccess.Repositories;
using ComeBien.Services;
using Serilog;
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
            Log.Information("Obteniendo datos iniciales en ShoppingCartVM");

            IList<ShoppingCartProductVM> shoppingCartProducts = new List<ShoppingCartProductVM>();  
            var shoppingCart = ShoppingCartService.GetInstance().ShoppingCart;
            
            TotalAmount = shoppingCart.TotalAmount;
            int index = 0;
            foreach(var product in shoppingCart.Products)
            {
                shoppingCartProducts.Add(new ShoppingCartProductVM
                {
                    Index = index,
                    IndexEdited = index,
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Ingredients = String.Join($"{Environment.NewLine}", product.Ingredients.Select(x => $"{x.Quantity} x {x.Name}"))
                });
                index++;
            }
            ProductsCollection = new ObservableCollection<ShoppingCartProductVM>(shoppingCartProducts);

            Log.Information("Datos iniciales cargados en ShoppingCartVM");
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
            try
            {
                if(ShoppingCartService.GetInstance().ShoppingCart.Products.Count == 0)
                {
                    MessageBox.Show(ComeBien.Resources.Resources.ResourceManager.GetString("CartEmpty"),
                            $"", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    return;
                }
                Log.Information("Intento de pagar la orden");
                _orderService.PayOrder();

                Log.Information("Orden pagada correctamente");

                ProductsCollection.Clear();
                TotalAmount = 0;

                Log.Information("Información del carrito View Model ha sido limpiada");

                MessageBox.Show(ComeBien.Resources.Resources.ResourceManager.GetString("ShoppingSuccess"),
                            $"{ComeBien.Resources.Resources.ResourceManager.GetString("Congrats")}!!!", MessageBoxButton.OK,
                            MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error pagando la orden");
                MessageBox.Show(ComeBien.Resources.Resources.ResourceManager.GetString("ErrorMessage"),
                            $"{ComeBien.Resources.Resources.ResourceManager.GetString("Error")}!!!", MessageBoxButton.OK,
                            MessageBoxImage.Error);
            }
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
            try
            {
                Log.Information("Intento de eliminar producto del carrito");
                var product = ProductsCollection.FirstOrDefault(x=>x.Index == index);
                
                int indexEdited = product.IndexEdited;
                TotalAmount -= product.Price;
                ProductsCollection.Remove(product);

                ProductsCollection.Where(x => x.Index > index).ToList().ForEach(x => x.IndexEdited -= 1);

                ShoppingCartService.GetInstance().RemoveProduct(indexEdited);

                Log.Information("Producto eliminado del carrito");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error eliminando el producto de index {index} del carrito");
                MessageBox.Show(ComeBien.Resources.Resources.ResourceManager.GetString("ErrorMessage"),
                            $"{ComeBien.Resources.Resources.ResourceManager.GetString("Error")}!!!", MessageBoxButton.OK,
                            MessageBoxImage.Error);
            }
            
        }
    }
}
