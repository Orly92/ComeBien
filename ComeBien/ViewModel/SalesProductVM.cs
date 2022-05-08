using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.ViewModel
{
    internal class SalesProductVM : NotificationClass
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IIngredientsRepository _ingredientsRepository;
        private ObservableCollection<ProductVM> productsCollection;
        private ObservableCollection<IngredientVM> ingredientsCollection;
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
    }
}
