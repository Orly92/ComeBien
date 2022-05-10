using ComeBien.DataAccess.Repositories;
using ComeBien.Services.FileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    public class PDFService : IFileService
    {
        private readonly IOrderRepository _orderRepository;
        public PDFService()
        {
            _orderRepository = new OrderRepository();
        }

        public Task ExportOrders(DateTime dateInitial, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }
    }
}
