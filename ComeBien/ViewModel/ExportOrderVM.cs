using ComeBien.Models.Globals;
using ComeBien.Services;
using ComeBien.Services.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    public class ExportOrderVM : NotificationClass
    {
        private ICommand _exportOrderCommand;
        private ExportOrderEnum _exportOrderType;
        private readonly IFileFactory _fileFactory;
        private DateTime _dateInitial;
        private DateTime _dateEnd;
        public ExportOrderVM(ExportOrderEnum type)
        {
            _exportOrderType = type;
            _fileFactory = new FileFactory();
            DateInitial = DateTime.Now.Date;
            DateEnd = DateTime.Now.Date.AddDays(1);
        }

        public DateTime DateInitial
        {
            get
            {
                return _dateInitial;
            }
            set
            {
                _dateInitial = value;
                OnPropertyChanged("DateInitial");
            }
        }
        public DateTime DateEnd
        {
            get
            {
                return _dateEnd;
            }
            set
            {
                _dateEnd = value;
                OnPropertyChanged("DateEnd");
            }
        }
        public ICommand ExportOrderCommand
        {
            get
            {
                if (_exportOrderCommand == null)
                    _exportOrderCommand = new RelayCommand(async param => await ExportOrderToFile((Window) param), true);

                return _exportOrderCommand;
            }
        }

        private async Task ExportOrderToFile(Window window)
        {
            IFileService fileService = _fileFactory.GetFileService(_exportOrderType);
            await fileService.ExportOrders(DateInitial, DateEnd);

            MessageBox.Show(ComeBien.Resources.Resources.ResourceManager.GetString("ExportSuccess"),
                            $"{ComeBien.Resources.Resources.ResourceManager.GetString("Congrats")}", MessageBoxButton.OK,
                            MessageBoxImage.Information);

            if(window != null)
                window.Close();
        }
    }
}
