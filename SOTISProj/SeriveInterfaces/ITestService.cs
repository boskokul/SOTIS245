using SOTISProj.DTO;
using SOTISProj.Repo;
namespace SOTISProj.SeriveInterfaces
{
    public interface ITestService
    {
        public Test createTest(string json_string, string field, string name);

        public TestSample CreateTestSample(TestSampleDTO testSampleDTO);
        public List<Test> getAllByField(string field);

        public List<TestSample> getAllByFieldTestSamples(string field);
    }
}
