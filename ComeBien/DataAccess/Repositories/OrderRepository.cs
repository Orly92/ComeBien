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

                    IList<OrderProductIngredients> orderProductsIng = new List<OrderProductIngredients>();
                    foreach(var product in shoppingCart.Products)
                    {
                        var orderProduct = new OrderProducts
                        {
                            OrderId = idOrder,
                            ProductId = product.ProductId,
                            Price = product.Price
                        };
                        await dbContext.OrderProducts.AddAsync(orderProduct);
                        await dbContext.SaveChangesAsync();

                        foreach (var ingredient in product.Ingredients)
                        {
                            var dbIng = dbIngredients.FirstOrDefault(x => x.Id == ingredient.IngredientId);
                            orderProductsIng.Add(new OrderProductIngredients
                            {
                                ItemId = orderProduct.ItemId,
                                IngredientId = ingredient.IngredientId,
                                Quantity = ingredient.Quantity,
                                EnName = dbIng?.EnName,
                                EsName = dbIng?.EsName,
                                FrName = dbIng?.FrName,
                            });
                        }
                    }

                    
                    await dbContext.OrderProductIngredients.AddRangeAsync(orderProductsIng);
                    await dbContext.SaveChangesAsync();

                    await databaseTransaction.CommitAsync();
                }
            }

            return idOrder;
        }

        public async Task<IList<OrderDTO>> GetOrders(DateTime? dateInitial, DateTime? dateEnd)
        {
            using (var dbContext = new ComeBienContext())
            {
                var query = dbContext.Orders.AsQueryable();
                if(dateInitial != null)
                {
                    query = query.Where(x => x.TimeStamp >= dateInitial);
                }
                if (dateEnd != null)
                {
                    query = query.Where(x => x.TimeStamp < dateEnd);
                }

                var resp = await query.Join(dbContext.OrderProducts,
                                    o => o.Id,
                                    op => op.OrderId,
                                    (o, op) => new
                                    {
                                        o.TimeStamp,
                                        o.Id,
                                        o.TotalAmount,
                                        op.ProductId,
                                        op.ItemId,
                                        ProductPrice = op.Price
                                    }).ToListAsync();

                return resp.GroupBy(x=>new {x.Id,x.TimeStamp,x.TotalAmount},(f,g)=> new OrderDTO
                {
                    Id = f.Id,
                    TimeStamp = f.TimeStamp,
                    TotalAmount = f.TotalAmount,
                    OrderProducts = g.Select(x => new OrderProductsDTO
                    {
                        ItemId = x.ItemId,
                        OrderId = x.Id,
                        Price = x.ProductPrice,
                        ProductId = x.ProductId,
                    }).ToList()
                }).OrderBy(x=>x.Id).ToList();
            }
        }

        public async Task<IList<OrderDTO>> GetOrdersWithIngredients(DateTime? dateInitial, DateTime? dateEnd)
        {
            var orders = await GetOrders(dateInitial, dateEnd);
            var idsItems = orders.SelectMany(x => x.OrderProducts).Select(x => x.ItemId).ToList();
            
            using (var dbContext = new ComeBienContext())
            {
                var orderIngredients = await dbContext.OrderProductIngredients.
                    Where(x => idsItems.Contains(x.ItemId)).ToListAsync();

                foreach(var order in orders)
                {
                    foreach(var product in order.OrderProducts)
                    {
                        product.Ingredients = orderIngredients.Where(x => x.ItemId == product.ItemId).ToList();
                    }
                }
            }

            return orders;
        }
    }

    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<int> CreateOrder(ShoppingCart shoppingCart);
        Task<IList<OrderDTO>> GetOrders(DateTime? dateInitial, DateTime? dateEnd);
        Task<IList<OrderDTO>> GetOrdersWithIngredients(DateTime? dateInitial, DateTime? dateEnd);
    }
}
