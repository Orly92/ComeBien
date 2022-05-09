using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Models.Database
{
    public class OrderProductIngredients
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int IngredientId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string EsName { get; set; }
        public string EnName { get; set; }
        public string FrName { get; set; }
    }
}
