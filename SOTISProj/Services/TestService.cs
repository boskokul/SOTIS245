using SOTISProj.SeriveInterfaces;
using SOTISProj.Repo;

namespace SOTISProj.Services
{
    public class TestService : ITestService
    {
        private MyDbContext _db;
        public TestService(MyDbContext db)
        {
            _db = db;
        }

        public Test createTest(string json_string)
        {
            throw new NotImplementedException();
        }

        
    }
}
