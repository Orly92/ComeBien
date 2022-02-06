using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public BaseRepository()
        {

        }

        public virtual async Task<IReadOnlyCollection<T>> GetAll()
        {
            using (var dbContext = new ComeBienContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();
                return await dbSet.ToListAsync();
            }
        }
    }

    public interface IBaseRepository<T> where T : class
    {
        Task<IReadOnlyCollection<T>> GetAll();
    }
}
