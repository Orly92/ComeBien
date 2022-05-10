using ComeBien.Models.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services.FileServices
{
    public class FileFactory : IFileFactory
    {
        public IFileService GetFileService(ExportOrderEnum exportOrderType)
        {
            switch (exportOrderType)
            {
                case ExportOrderEnum.PDF:
                    return new PDFService();

                case ExportOrderEnum.JSON:
                    return new JsonFileService();
            }

            throw new NotImplementedException();
        }
    }

    public interface IFileFactory
    {
        IFileService GetFileService(ExportOrderEnum exportOrderType);
    }
}
