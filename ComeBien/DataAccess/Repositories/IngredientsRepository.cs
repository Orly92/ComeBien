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

        public async Task Delete(int id)
        {
            using (var dbContext = new ComeBienContext())
            {
                Ingredients ingredient = await dbContext.Ingredients.FindAsync(id);
                if(ingredient != null)
                {
                    dbContext.Remove(ingredient);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }

    public interface IIngredientsRepository : IBaseRepository<Ingredients>
    {
        Task Delete(int id);
    }
}
