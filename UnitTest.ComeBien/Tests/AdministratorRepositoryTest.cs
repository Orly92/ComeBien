using ComeBien.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitTest.ComeBien.Tests
{

    [TestClass]
    public class AdministratorRepositoryTest
    {
        private readonly IAdministratorRepository _administratorRepository;

        public AdministratorRepositoryTest()
        {
            _administratorRepository = new AdministratorRepository();
        }

        [TestMethod]
        public async Task GetAdminFoundedTest()
        {
            string username = "admin";
            string pass = "admin1234*";

            var admin = await _administratorRepository.GetAdmin(username, pass);

            Assert.IsNotNull(admin);
        }

        [TestMethod]
        public async Task GetAdminNotFoundedTest()
        {
            string username = "adminaaa";
            string pass = "admin1234*";

            var admin = await _administratorRepository.GetAdmin(username, pass);

            Assert.IsNull(admin);
        }
    }
}
