using SOTISProj.Repo;
namespace SOTISProj.RepositoryInterfaces
{
    public interface ITestRepository
    {
        public Test save(Test test);
        public List<Test> getAllByFIeld(string field);
    }
}
