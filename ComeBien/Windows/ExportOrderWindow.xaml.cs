using ComeBien.Models.Globals;
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
using System.Windows.Shapes;

namespace ComeBien.Windows
{
    /// <summary>
    /// Interaction logic for ExportOrderWindow.xaml
    /// </summary>
    public partial class ExportOrderWindow : Window
    {
        public ExportOrderWindow(ExportOrderEnum exportOrderType)
        {
            InitializeComponent();

            this.DataContext = new ExportOrderVM(exportOrderType);
        }
    }
}
