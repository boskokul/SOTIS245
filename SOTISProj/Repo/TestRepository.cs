using Microsoft.EntityFrameworkCore;
using SOTISProj.RepositoryInterfaces;

namespace SOTISProj.Repo
{
    public class TestRepository : ITestRepository
    {
        private MyDbContext _context;
        public TestRepository(MyDbContext myDbContext) {
            _context = myDbContext;
        }

        public List<Test> getAllByFIeld(string field)
        {
            return _context.Tests.Include(t => t.ConnectQuestions).Include(t => t.DefinitionQuestions).Include(t => t.Field).Where(t => t.Field.Name == field).ToList();  
        }

        public Test save(Test test)
        {
            _context.Tests.Add(test);
            _context.SaveChanges();
            return test;
        }
    }
}
