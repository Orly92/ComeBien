using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services.FileServices
{
    public interface IFileService
    {
        Task ExportOrders(DateTime dateInitial, DateTime dateEnd);
    }
}
