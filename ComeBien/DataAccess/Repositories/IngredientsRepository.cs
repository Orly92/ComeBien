using ComeBien.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.DataAccess.Repositories
{
    public class IngredientsRepository : BaseRepository<Ingredients>, IIngredientsRepository
    {
        public IngredientsRepository()
        {
                
        }
    }

    public interface IIngredientsRepository : IBaseRepository<Ingredients>
    {
    }
}
