using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Database;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService()
        {
            _orderRepository = new OrderRepository();   
        }

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task PayOrder()
        {
            int idOrder = await _orderRepository.CreateOrder(ShoppingCartService.GetInstance().ShoppingCart);

            Log.Information($"Orden creada con id {idOrder}");

            ShoppingCartService.GetInstance().Reset();

            Log.Information("Carrito en servicio reseteado");
        }

    }

    public interface IOrderService
    {
        Task PayOrder();
    }
}
