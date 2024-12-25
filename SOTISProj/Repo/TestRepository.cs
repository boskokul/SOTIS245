using Microsoft.EntityFrameworkCore;
using SOTISProj.DTO;
using SOTISProj.RepositoryInterfaces;
using static System.Net.Mime.MediaTypeNames;

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

        public List<TestSample> getAllByFIeldTestSamples(string field)
        {
            return _context.TestSamples.Include(t => t.DefinitionAnswers).ThenInclude(da => da.Question).Include(t => t.ConnectAnswers).ThenInclude(da => da.Question).Include(t => t.Test).ThenInclude(da => da.Field).Include(t => t.Student).ThenInclude(da => da.User).Where(t => t.Test.Field.Name == field).ToList();
        }

        public Test save(Test test)
        {
            _context.Tests.Add(test);
            _context.SaveChanges();
            return test;
        }

        public TestSample Save(TestSampleDTO testSampleDTO)
        {
            var test = _context.Tests.First(t => t.Id == testSampleDTO.TestId);
            var testSample = new TestSample();
            testSample.TakenOn = DateTime.UtcNow;
            testSample.Test = test;
            testSample.Student = _context.Students.Include(t => t.User).First(t => t.Id == testSampleDTO.StudentId);
            foreach (var defA in testSampleDTO.DefinitionAnswers)
            {
                var defAnsw = new DefinitionAnswer();
                defAnsw.Test = test;
                defAnsw.Question = _context.DefinitionQuestions.First(t => t.Id == defA.DefQuestionId);
                defAnsw.AnsweredDefinition = defA.AnsweredDefinition;
                defAnsw.IsCorrect = defA.IsCorrect;
                defAnsw.TestSample = testSample;
                testSample.DefinitionAnswers.Add(defAnsw);
            }
            foreach (var conA in testSampleDTO.ConnectAnswers)
            {
                var conAnsw = new ConnectAnswer();
                conAnsw.Test = test;
                conAnsw.Question = _context.ConnectQuestions.First(t => t.Id == conA.ConnectQuestionId);
                conAnsw.ConnectedPairs = conA.ConnectedPairs;
                conAnsw.TestSample = testSample;
                testSample.ConnectAnswers.Add(conAnsw);
            }
            _context.TestSamples.Add(testSample);
            _context.SaveChanges();
            return testSample;
        }
    }
}
