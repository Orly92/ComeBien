using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Core;
using ComeBien.Models.Database;
using ComeBien.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    internal class SalesProductVM : NotificationClass
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
            var products = _productsRepository.GetAllSync().Select(x => new ProductVM(x));
            productsCollection = new ObservableCollection<ProductVM>(products);

            var ingredients = _ingredientsRepository.GetAllSync().Select(x => new IngredientVM(x));
            ingredientsCollection = new ObservableCollection<IngredientVM>(ingredients);
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
            decimal totalAmount = ingredients.Sum(x => x.Quantity * x.Price) + (ProductsCollection
                                .FirstOrDefault(x=>x.Id == productId)?.Price ?? 0);

            ShoppingCartProduct shoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                Price = totalAmount,
                Ingredients = ingredients.Select(x => new ShoppingCartIngredients
                {
                    IngredientId = x.Id,
                    Quantity = x.Quantity
                }).ToList()
            };

            ShoppingCartService.GetInstance().AddProduct(shoppingCartProduct);
            IngredCollection.Where(x => x.Quantity > 0).ToList().ForEach(x => x.ResetQuantity());

        }
    }
}
