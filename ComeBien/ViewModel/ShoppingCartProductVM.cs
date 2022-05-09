using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    public class ShoppingCartProductVM
    {
        
        public ShoppingCartProductVM()
        {

        }

        public int Index { get; set; }
        public int IndexEdited { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Ingredients { get; set; }

        
    }
}
