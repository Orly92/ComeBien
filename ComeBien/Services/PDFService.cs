using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    public class PDFService : IPDFService
    {
        public PDFService()
        {

        }

        public Task ExportOrders(DateTime dateInitial, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPDFService
    {
        Task ExportOrders(DateTime dateInitial, DateTime dateEnd);
    }
}
