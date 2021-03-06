using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Core;
using ComeBien.Models.Database;
using ComeBien.Services.FileServices;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    public class JsonFileService : IFileService
    {
        private readonly IOrderRepository _orderRepository;
        public JsonFileService()
        {
            _orderRepository = new OrderRepository();
        }
        public async Task ExportOrders(DateTime dateInitial, DateTime dateEnd)
        {
            Log.Information($"Salvando documento JSON para las ordenes de {dateInitial} a {dateEnd}");

            IList<OrderDTO> orders = await _orderRepository.GetOrdersWithIngredients(dateInitial, dateEnd);
            
            Log.Information("Ordenes extraidas");
            
            await SaveFile("Resources/Files/orders.json", JsonConvert.SerializeObject(orders));

            Log.Information("Documento JSON salvado");
        }

        private async Task SaveFile(string path, string content)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            file.Directory.Create();
            await System.IO.File.WriteAllTextAsync(file.FullName, content);
        }
    }
}
