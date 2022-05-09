using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.ViewModel
{
    internal class ShoppingCartProductVM
    {
        public ShoppingCartProductVM()
        {

        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Ingredients { get; set; }
    }
}
