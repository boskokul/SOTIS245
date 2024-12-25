using Microsoft.AspNetCore.Mvc;
using SOTISProj.DTO;
using SOTISProj.SeriveInterfaces;
using System.Diagnostics;

namespace SOTISProj.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("{field}")]
        public ActionResult GetByField(string field)
        {
            return Ok(_testService.getAllByField(field));
        }

        [HttpPost("createTestSample")]
        public IActionResult CreateTestSample(TestSampleDTO testSampleDTO)
        {
            foreach (var conAnsw in testSampleDTO.ConnectAnswers)
            {
                foreach (var cA in conAnsw.ConnectedPairs)
                {
                    string finalResult = RunPythonScript("..\\SOTISProj\\PythonScripts\\check_connect.py", $"\"{cA.BelongTerm}\" \"{cA.BelongingTerm}\"");
                    if (finalResult == null)
                        return BadRequest("Error grading connect answers.");
                    if (finalResult.StartsWith("correct"))
                        cA.IsCorrect = true;
                }
            }
            foreach (var defAnsw in testSampleDTO.DefinitionAnswers)
            {
                string finalResult = RunPythonScript("..\\SOTISProj\\PythonScripts\\check_def.py", $"\"{defAnsw.Term}\" \"{defAnsw.AnsweredDefinition}\"");
                if (finalResult == null)
                    return BadRequest("Error grading definition answers.");
                if (finalResult.StartsWith("Correct"))
                    defAnsw.IsCorrect = true;
                
            }
            return Ok(_testService.CreateTestSample(testSampleDTO));
        }


        private string RunPythonScript(string scriptPath, string arguments)
        {
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe",
                Arguments = $"{scriptPath} {arguments}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string errors;
            string output;

            try
            {
                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        output = reader.ReadToEnd();
                    }
                    errors = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        Debug.WriteLine($"Standard Error: {errors}");
                        return null;
                    }
                }

                Debug.WriteLine($"Standard Output: {output}");
                return output;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        [HttpGet("TestSamples/{field}")]
        public ActionResult GetByFieldTestSamples(string field)
        {
            return Ok(_testService.getAllByFieldTestSamples(field));
        }
    }
}
