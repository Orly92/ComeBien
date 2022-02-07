using ComeBien.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComeBien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductsRepository _productsRepository;
        private CollectionViewSource productsViewSource;
        public MainWindow()
        {
            InitializeComponent();
            _productsRepository = new ProductsRepository();

        }

        private async void productsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            await SetProducts();
        }

        private async Task SetProducts()
        {
            productsViewSource.Source = await _productsRepository.GetAll();
        }
    }
}
