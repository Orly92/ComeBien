using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Models.Core
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal TotalAmount { get; set; }

        public IList<OrderProductsDTO> OrderProducts { get; set; }
    }
}
