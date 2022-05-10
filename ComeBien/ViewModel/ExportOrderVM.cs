using ComeBien.Models.Globals;
using ComeBien.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    public class ExportOrderVM : NotificationClass
    {
        private ICommand _exportOrderCommand;
        private ExportOrderEnum _exportOrderType;
        private readonly IPDFService _pdfService;
        private DateTime _dateInitial;
        private DateTime _dateEnd;
        public ExportOrderVM(ExportOrderEnum type)
        {
            _exportOrderType = type;
            _pdfService = new PDFService();
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
                    _exportOrderCommand = new RelayCommand(async param => await ExportOrderToFile(), true);

                return _exportOrderCommand;
            }
        }

        private async Task ExportOrderToFile()
        {
            switch (_exportOrderType)
            {
                case ExportOrderEnum.PDF:
                    await _pdfService.ExportOrders(DateInitial, DateEnd);
                    break;
            }
        }
    }
}
