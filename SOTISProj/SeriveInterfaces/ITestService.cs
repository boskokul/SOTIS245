using SOTISProj.Repo;
namespace SOTISProj.SeriveInterfaces
{
    public interface ITestService
    {
        public Test createTest(string json_string, string field, string name);
    }
}
