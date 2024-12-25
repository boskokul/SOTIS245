using SOTISProj.DTO;
using SOTISProj.Repo;
namespace SOTISProj.RepositoryInterfaces
{
    public interface ITestRepository
    {
        public Test save(Test test);
        public TestSample Save(TestSampleDTO test);
        public List<Test> getAllByFIeld(string field);
        public List<TestSample> getAllByFIeldTestSamples(string field);
    }
}
