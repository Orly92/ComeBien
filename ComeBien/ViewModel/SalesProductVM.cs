using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Core;
using ComeBien.Models.Database;
using ComeBien.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    public class SalesProductVM : NotificationClass
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IIngredientsRepository _ingredientsRepository;
        private ObservableCollection<ProductVM> productsCollection;
        private ObservableCollection<IngredientVM> ingredientsCollection;
        private ICommand _addProductCommand;
        public SalesProductVM()
        {
            _productsRepository = new ProductsRepository();
            _ingredientsRepository = new IngredientsRepository();
            InitializeCollection();
        }

        private void InitializeCollection()
        {
            Log.Information($"Obteniendo los datos iniciales de SalesProductVM");

            var products = _productsRepository.GetAllSync().Select(x => new ProductVM(x));
            productsCollection = new ObservableCollection<ProductVM>(products);

            var ingredients = _ingredientsRepository.GetAllSync().Select(x => new IngredientVM(x));
            ingredientsCollection = new ObservableCollection<IngredientVM>(ingredients);

            
            Log.Information("Datos iniciales cargados en SalesProductVM");
        }

        public ObservableCollection<ProductVM> ProductsCollection
        {
            get { return productsCollection; }
            set
            {
                productsCollection = value;
                OnPropertyChanged("ProductsCollection");
            }
        }

        public ObservableCollection<IngredientVM> IngredCollection
        {
            get { return ingredientsCollection; }
            set
            {
                ingredientsCollection = value;
                OnPropertyChanged("IngredCollection");
            }
        }

        public ICommand AddProductCommand
        {
            get
            {
                if (_addProductCommand == null)
                    _addProductCommand = new RelayCommand(param => AddProductToShoppingCart((int)param), true);

                return _addProductCommand;
            }
        }

        private void AddProductToShoppingCart(int productId)
        {
            
            var ingredients = IngredCollection.Where(x=>x.Quantity > 0).ToList();
            var product = ProductsCollection.FirstOrDefault(x => x.Id == productId);
            decimal totalAmount = ingredients.Sum(x => x.Quantity * x.Price) + (product?.Price ?? 0);

            ShoppingCartProduct shoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                Price = totalAmount,
                Name = product?.Name,
                Ingredients = ingredients.Select(x => new ShoppingCartIngredients
                {
                    IngredientId = x.Id,
                    Quantity = x.Quantity,
                    Name = x.Name
                }).ToList()
            };

            Log.Information($"Adicionando el producto {productId}-{shoppingCartProduct.Name} " +
                $"con los ingredientes -> {String.Join(",", shoppingCartProduct.Ingredients.Select(x => $"{x.Quantity} x {x.Name}"))} al carrito");

            ShoppingCartService.GetInstance().AddProduct(shoppingCartProduct);
            IngredCollection.Where(x => x.Quantity > 0).ToList().ForEach(x => x.ResetQuantity());

            Log.Information("Producto anterior adicionado al carrito");
        }
    }
}
