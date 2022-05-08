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
    internal class ProductVM : NotificationClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }

        public ProductVM()
        {

        }

        public ProductVM(Products product)
        {
            Id = product.Id;
            Price = Math.Round(product.Price,2);

            switch (ConfigService.lang)
            {
                case Languages.France:
                    Name = product.FrName;
                    Desc = product.FrDescription;
                    break;
                case Languages.English:
                    Name = product.EnName;
                    Desc = product.EnDescription;
                    break;
                default:
                    Name = product.EsName;
                    Desc = product.EsDescription;
                    break;
            }
        }
    }
}
