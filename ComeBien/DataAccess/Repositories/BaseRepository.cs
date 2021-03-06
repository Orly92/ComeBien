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

        public virtual async Task<IList<T>> GetAll()
        {
            using (var dbContext = new ComeBienContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();
                return await dbSet.ToListAsync();
            }
        }

        public virtual async Task<T> Add(T entity)
        {
            using (var dbContext = new ComeBienContext())
            {
                await dbContext.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return entity;
            }
        }

        public virtual async Task Delete(T entity)
        {
            using (var dbContext = new ComeBienContext())
            {
                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task<T> Update(T entity)
        {
            using (var dbContext = new ComeBienContext())
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                return entity;
            }
        }

        public virtual async Task<T> FindById(int id)
        {
            using (var dbContext = new ComeBienContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();
                return await dbSet.FindAsync(id);
            }
        }

        public virtual IList<T> GetAllSync()
        {
            using (var dbContext = new ComeBienContext())
            {
                DbSet<T> dbSet = dbContext.Set<T>();
                return dbSet.ToList();
            }
        }
    }

    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAll();
        IList<T> GetAllSync();
        Task<T> Add(T entity);
        Task Delete(T entity);
        Task<T> Update(T entity);
        Task<T> FindById(int id);
    }
}
