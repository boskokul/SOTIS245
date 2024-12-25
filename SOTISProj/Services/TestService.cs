using SOTISProj.SeriveInterfaces;
using SOTISProj.Repo;
using Newtonsoft.Json.Linq;
using SOTISProj.RepositoryInterfaces;
using SOTISProj.DTO;
using System.Diagnostics;

namespace SOTISProj.Services
{
    public class TestService : ITestService
    {
        private ITestRepository _repository;
        IFieldRepository _fieldRepository;
        public TestService(ITestRepository repository, IFieldRepository fieldRepository)
        {
            _repository = repository;
            _fieldRepository = fieldRepository;
        }

        public Test createTest(string json_string, string field, string name)
        {
            JObject loadedData = JObject.Parse(json_string);
            List<ConnectQuestion> connectQuestions = getConnectQuestions(loadedData);
            List<DefinitionQuestion> definitionQuestions = GetDefinitionQuestions(loadedData);

            Test newTest = new Test();
            newTest.ConnectQuestions = connectQuestions;
            newTest.DefinitionQuestions = definitionQuestions;
            newTest.Name = name;
            newTest.Field = _fieldRepository.getByRootTerm(field);

            Test savedTest =_repository.save(newTest);
            return savedTest;
        }

        public TestSample CreateTestSample(TestSampleDTO testSampleDTO)
        {                
            TestSample savedTest = _repository.Save(testSampleDTO);
            return savedTest;
        }


        private List<ConnectQuestion> getConnectQuestions(JObject loadedData)
        {
            JArray connectQuestions_json = (JArray)loadedData["connect_questions"];
            List<ConnectQuestion> connectQuestions = new List<ConnectQuestion>();
            foreach (var question in connectQuestions_json)
            {
                JArray terms = (JArray)question["terms"];
                JArray parents = (JArray)question["parents"];

                List<string> termsList = terms.ToObject<List<String>>();
                List<string> parentsList = parents.ToObject<List<String>>();
                ConnectQuestion connectQuestion = new ConnectQuestion(termsList, parentsList);
                connectQuestions.Add(connectQuestion);
            }

            return connectQuestions;
        }

        private List<DefinitionQuestion> GetDefinitionQuestions(JObject loadedData)
        {
            JArray defQuestions_json = (JArray)loadedData["def_terms"];
            List<string> defQUestionsList = defQuestions_json.ToObject<List<String>>();

            List<DefinitionQuestion> definitionQuestions = new List<DefinitionQuestion>();
            foreach (string defQuestion in defQUestionsList)
            {
                DefinitionQuestion definitionQuestion = new DefinitionQuestion(defQuestion);
                definitionQuestions.Add(definitionQuestion);
            }
            return definitionQuestions;
        }
        public List<Test> getAllByField(string field)
        {
            return _repository.getAllByFIeld(field);
        }

        public List<TestSample> getAllByFieldTestSamples(string field)
        {
            return _repository.getAllByFIeldTestSamples(field);
        }
    }
}
