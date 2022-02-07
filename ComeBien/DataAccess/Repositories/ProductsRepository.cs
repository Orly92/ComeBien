using ComeBien.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.DataAccess.Repositories
{
    public class ProductsRepository : BaseRepository<Products>, IProductsRepository
    {
        public ProductsRepository()
        {

        }
    }

    public interface IProductsRepository : IBaseRepository<Products>
    {
    }
}
