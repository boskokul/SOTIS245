using SOTISProj.RepositoryInterfaces;

namespace SOTISProj.Repo
{
    public class TestRepository : ITestRepository
    {
        private MyDbContext _context;
        public TestRepository(MyDbContext myDbContext) {
            _context = myDbContext;
        }
        public Test save(Test test)
        {
            _context.Tests.Add(test);
            _context.SaveChanges();
            return test;
        }
    }
}
