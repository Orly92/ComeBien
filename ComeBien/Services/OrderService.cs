using ComeBien.DataAccess.Repositories;
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

        public async Task PayOrder()
        {
            await _orderRepository.CreateOrder(ShoppingCartService.GetInstance().ShoppingCart);

            ShoppingCartService.GetInstance().Reset();
        }
    }

    public interface IOrderService
    {
        Task PayOrder();
    }
}
