using ComeBien.Models.Database;
using ComeBien.Models.Globals;
using ComeBien.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.ViewModel
{
    internal class IngredientVM
    {
        public IngredientVM()
        {

        }
        public IngredientVM(Ingredients x)
        {
            Id = x.Id;
            Price = Math.Round(x.Price,2);
            switch (ConfigService.lang)
            {
                case Languages.France:
                    Name = x.FrName;
                    break;
                case Languages.English:
                    Name = x.EnName;
                    break;
                default:
                    Name = x.EsName;
                    break;
            }
            Quantity = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
