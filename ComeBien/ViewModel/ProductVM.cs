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
    public class ProductVM : NotificationClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public string PriceStr { get; set; }
        public string Image { get; set; }

        public ProductVM()
        {

        }

        public ProductVM(Products product)
        {
            Id = product.Id;
            Price = Math.Round(product.Price,2);
            Image = $"../Resources/Images/Products/{Id}.png";
            switch (ConfigService.lang)
            {
                case Languages.France:
                    Name = product.FrName;
                    Desc = product.FrDescription;
                    PriceStr = $"Depuis {Price}€";
                    break;
                case Languages.English:
                    Name = product.EnName;
                    Desc = product.EnDescription;
                    PriceStr = $"Since {Price}€";
                    break;
                default:
                    Name = product.EsName;
                    Desc = product.EsDescription;
                    PriceStr = $"Desde {Price}€";
                    break;
            }
        }
    }
}
