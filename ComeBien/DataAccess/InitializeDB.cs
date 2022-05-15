using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.DataAccess
{
    public class InitializeDB : IInitializeDB
    {
        public InitializeDB()
        {

        }

        public void Initialize()
        {
            using (var dbContext = new ComeBienContext())
            {
                if (dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }

    public interface IInitializeDB
    {
        void Initialize();
    }
}
