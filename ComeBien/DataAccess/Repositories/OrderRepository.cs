using ComeBien.Models.Core;
using ComeBien.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.DataAccess.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository()
        {

        }

        public async Task<int> CreateOrder(ShoppingCart shoppingCart)
        {
            int idOrder = 0;
            using (var dbContext = new ComeBienContext())
            {
                IList<int> idIngredients = shoppingCart.Products.SelectMany(x=>x.Ingredients)
                    .Select(x=>x.IngredientId).Distinct().ToList();
                var dbIngredients = await dbContext.Ingredients.Where(x => idIngredients.Contains(x.Id)).ToListAsync();
                
                using (var databaseTransaction = dbContext.Database.BeginTransaction())
                {
                    var order = new Order
                    {
                        TimeStamp = DateTime.Now,
                        TotalAmount = shoppingCart.TotalAmount
                    };

                    await dbContext.Orders.AddAsync(order);
                    await dbContext.SaveChangesAsync();

                    idOrder = order.Id;

                    IList<OrderProducts> orderProducts = new List<OrderProducts>();
                    IList<OrderProductIngredients> orderProductsIng = new List<OrderProductIngredients>();
                    foreach(var product in shoppingCart.Products)
                    {
                        orderProducts.Add(new OrderProducts
                        {
                            OrderId = idOrder,
                            ProductId = product.ProductId,
                            Price = product.Price
                        });

                        foreach(var ingredient in product.Ingredients)
                        {
                            var dbIng = dbIngredients.FirstOrDefault(x => x.Id == ingredient.IngredientId);
                            orderProductsIng.Add(new OrderProductIngredients
                            {
                                OrderId = idOrder,
                                ProductId = product.ProductId,
                                IngredientId = ingredient.IngredientId,
                                Quantity = ingredient.Quantity,
                                EnName = dbIng?.EnName,
                                EsName = dbIng?.EsName,
                                FrName = dbIng?.FrName,
                            });
                        }
                    }

                    await dbContext.OrderProducts.AddRangeAsync(orderProducts);
                    await dbContext.OrderProductIngredients.AddRangeAsync(orderProductsIng);
                    await dbContext.SaveChangesAsync();

                    await databaseTransaction.CommitAsync();
                }
            }

            return idOrder;
        }
    }

    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<int> CreateOrder(ShoppingCart shoppingCart);
    }
}
