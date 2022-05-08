using ComeBien.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.ViewModel
{
    internal class ShoppingCartMenuVM : NotificationClass
    {
        public ShoppingCartMenuVM()
        {

        }

        public int ShoppingCartCount
        {
            get { return ShoppingCartService.GetInstance().CountProduct(); }
        }

        public void EditShoppingCart()
        {
            OnPropertyChanged("ShoppingCartCount");
        }
    }
}
