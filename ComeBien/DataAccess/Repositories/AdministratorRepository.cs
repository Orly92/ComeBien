using ComeBien.Models.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
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

        public async Task<Administrator> GetAdmin(string userName, string pass)
        {
            Log.Information(ComeBien.Resources.Resources.ResourceManager.GetString("GetAdmin_Log"));

            using (var dbContext = new ComeBienContext())
            {
                return await dbContext.Administrators.
                    FirstOrDefaultAsync(x => x.UserName == userName && x.Password == pass);
            }
        }
    }

    public interface IAdministratorRepository : IBaseRepository<Administrator>
    {
        Task<Administrator> GetAdmin(string userName, string pass);
    }
}
