using ComeBien.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.DataAccess.Repositories
{
    public class AdministratorRepository : BaseRepository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository()
        {

        }

        public bool IsAdmin(string userName, string pass)
        {
            using (var dbContext = new ComeBienContext())
            {
                return dbContext.Administrators.Any(x => x.UserName == userName && x.Password == pass);
            }
        }
    }

    internal interface IAdministratorRepository : IBaseRepository<Administrator>
    {
    }
}
