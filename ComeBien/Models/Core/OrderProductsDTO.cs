using ComeBien.Models.Database;
using System.Collections.Generic;

namespace ComeBien.Models.Core
{
    public class OrderProductsDTO
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public IList<OrderProductIngredients> Ingredients { get; set; }
    }
}