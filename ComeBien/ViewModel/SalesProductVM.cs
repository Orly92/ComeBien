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
        private ObservableCollection<ProductVM> productsCollection;
        public SalesProductVM()
        {
            _productsRepository = new ProductsRepository();
            InitializeCollection();
        }

        private async Task InitializeCollectionAsync()
        {
            var products = await _productsRepository.GetAll();
            productsCollection = new ObservableCollection<ProductVM>(
                                    products.Select(x=>new ProductVM(x)));
        }

        private void InitializeCollection()
        {
            var products = _productsRepository.GetAllSync();
            productsCollection = new ObservableCollection<ProductVM>(
                                    products.Select(x => new ProductVM(x)));
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
    }
}
