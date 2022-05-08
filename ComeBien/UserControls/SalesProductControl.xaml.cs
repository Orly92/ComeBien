using ComeBien.ViewModel;
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

namespace ComeBien.UserControls
{
    /// <summary>
    /// Interaction logic for SalesProductControl.xaml
    /// </summary>
    public partial class SalesProductControl : UserControl
    {
        public SalesProductControl()
        {
            InitializeComponent();

            var salesProductVM = new SalesProductVM();
            this.DataContext = salesProductVM;
        }
    }
}
