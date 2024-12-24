using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOTISProj.DTO;
using SOTISProj.Repo;
using SOTISProj.SeriveInterfaces;
using SOTISProj.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace SOTISProj.Controllers
{
    [Route("api/python")]
    [ApiController]
    public class PythonController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly MyDbContext _context;
        private readonly ITestService _testService;
        public PythonController(IDataService dataService, MyDbContext context, ITestService testService)
        {
            _dataService = dataService;
            _context = context;
            _testService = testService;
        }
        [HttpGet("parsePDF")]
        public IActionResult RunParse()
        {
            var scriptPath = "..\\SOTISProj\\PythonScripts\\parsePDF.py";
            var pdfPath = "..\\SOTISProj\\PDFs\\networks2.pdf";
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe", 
                Arguments = $"{scriptPath} {pdfPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true, // Capture standard error
                UseShellExecute = false,
                CreateNoWindow = true
            };
            string errors;
            string ParsedText;
            try
            {
                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        ParsedText = reader.ReadToEnd();
                    }
                    errors = process.StandardError.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(ParsedText))
                    {
                        Debug.WriteLine($"Standard Output: {ParsedText}");
                        _dataService.SavePDFContent(ParsedText);
                    }
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        Debug.WriteLine($"Standard Error: {errors}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    return BadRequest($"Error running Python script: {errors}");
                }

                return Ok(ParsedText);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        
        [HttpGet("ExtractTerms")]
        public IActionResult RunTermsExraction()
        {
            var scriptPath = "..\\SOTISProj\\PythonScripts\\first.py";
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe",
                Arguments = $"{scriptPath} \"{_dataService.GetPDFContent()}\" \"{_dataService.GetAcmSubTree()}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string errors;
            string res;
            try
            {
                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        res = reader.ReadToEnd();
                    }
                    errors = process.StandardError.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        Debug.WriteLine($"Standard Output: {res}");
                        string jsonString1 = res.Split("###")[0];
                        string jsonString2 = res.Split("###")[1];

                        jsonString1 = jsonString1.Replace('\'', '"');

                        Dictionary<string, string> definitions = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString1);
                        _dataService.SaveTermsDefinitions(definitions);

                        foreach (var item in definitions)
                        {
                            Console.WriteLine($"{item.Key}: {item.Value}\n");
                        }

                        jsonString2 = jsonString2.Replace('\'', '"');

                        var relations = JsonSerializer.Deserialize<Dictionary<string, RelatedTerms>>(jsonString2, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        _dataService.SaveTermsRelations(relations);

                        foreach (var item in relations)
                        {
                            Console.WriteLine($"{item.Key}:");
                            foreach (var relatedTerm in item.Value.Related_to)
                            {
                                Console.WriteLine($"  - {relatedTerm}");
                            }
                            Console.WriteLine();
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        Debug.WriteLine($"Standard Error: {errors}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    return BadRequest($"Error running Python script: {errors}");
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("FindNewRelations")]
        public IActionResult RunTermsDefinitions()
        {
            string terms = "";
            foreach(var t  in _dataService.GetTermsRelations())
            {
                terms += t.Key + ", ";
            }
            var scriptPath = "..\\SOTISProj\\PythonScripts\\second.py";
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe",
                Arguments = $"{scriptPath} \"{terms}\" \"{_dataService.GetAcmSubTree()}\" \"{_dataService.GetTermsRelations()}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            string errors;
            string TermsJSON;
            try
            {
                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        TermsJSON = reader.ReadToEnd();
                    }
                    errors = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(TermsJSON))
                    {
                        Debug.WriteLine($"Standard Output: {TermsJSON}");
                    }
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        Debug.WriteLine($"Standard Error: {errors}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    return BadRequest($"Error running Python script: {errors}");
                }

                return Ok(TermsJSON);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("AddRelations")]
        public IActionResult RunTermsRelationsAdding()
        {
            string terms = "";
            foreach (var t in _dataService.GetTermsRelations())
            {
                terms += t.Key + ", ";
            }
            var scriptPath = "..\\SOTISProj\\PythonScripts\\third.py";
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe",
                Arguments = $"{scriptPath} \"{terms}\" \"{_dataService.GetTermsRelationsPairs()}\"  \"{_dataService.GetTermsDefinitionsPairs()}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            string errors;
            string TermsJSON;
            try
            {
                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        TermsJSON = reader.ReadToEnd();
                    }
                    errors = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(TermsJSON))
                    {
                        Debug.WriteLine($"Standard Output: {TermsJSON}");
                    }
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        Debug.WriteLine($"Standard Error: {errors}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    return BadRequest($"Error running Python script: {errors}");
                }

                return Ok(TermsJSON);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("acmPart")]
        public IActionResult GetACM()
        {
            var scriptPath = "..\\SOTISProj\\PythonScripts\\database_test.py";
            var subTree = "Networks";
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe",
                Arguments = $"{scriptPath} \"{subTree}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            
            string result;
            string errors;

            try
            {
                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                    errors = process.StandardError.ReadToEnd(); // Capture errors

                    // Log output and errors for debugging
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        Debug.WriteLine($"Standard Output: {result}");
                        _dataService.SaveAcmSubTree(result.Split("###")[1]);
                    }
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        Debug.WriteLine($"Standard Error: {errors}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    return BadRequest($"Error running Python script: {errors}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("subject")] 
        public IActionResult PostMyModel(Field myModel) 
        { 
            _context.Fields.Add(myModel); 
            _context.SaveChanges(); 
            return Ok(new { id = myModel.Id, name = myModel.Name });

        }

        [HttpPost("uploadPDF/{rootTerm}")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, string rootTerm)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            InstancePDF instancePDF = new InstancePDF();
            instancePDF.Field = _context.Fields.ToList().Find(x => x.RootTerm.Equals(rootTerm));
            instancePDF.Name = file.FileName;
            _context.instancePDFs.Add(instancePDF);
            _context.SaveChanges();

            return Ok(new { FilePath = filePath });
        }

        [HttpGet("getPDF/{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            Response.Headers.Add("Content-Disposition", "inline; filename=" + fileName);
            return File(memory, "application/pdf");
        }

        [HttpGet("getPDFByTerm/{term}")]
        public async Task<IActionResult> GetFileByTerm(string term)
        {
            var fileName = _context.termPdfs.First(x => x.Term.Equals(term)).Pdf;
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            Response.Headers.Add("Content-Disposition", "inline; filename=" + fileName);
            return File(memory, "application/pdf");
        }

        [HttpGet("getAllPDFs/{rootTerm}")]
        public IActionResult GetAllPDFs(string rootTerm)
        {
            var res = new List<string>();
            var iinstancePDFs = _context.instancePDFs.Include(x => x.Field).ToList().FindAll(x => x.Field.RootTerm.Equals(rootTerm));
            foreach ( var ik in iinstancePDFs)
            {
                res.Add(ik.Name);
            }

            return Ok(res);
        }

        [HttpGet("acmSubtree/{rootTerm}")]
        public IActionResult GetACMSubtree(string rootTerm)
        {
            var scriptPath = "..\\SOTISProj\\PythonScripts\\acmSubtree.py";
            var subTree = rootTerm;
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe",
                Arguments = $"{scriptPath} \"{subTree}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            string result;
            string errors;

            try
            {
                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                    errors = process.StandardError.ReadToEnd(); 

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        Debug.WriteLine($"Standard Output: {result}");
                    }
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        Debug.WriteLine($"Standard Error: {errors}");
                    }
                }
                if (!string.IsNullOrWhiteSpace(errors))
                {
                    return BadRequest($"Error running Python script: {errors}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("createTest")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTest(TestParametersDTO testParameters)
        {
            string connectQuestionNums_json = JsonSerializer.Serialize(testParameters.ConQuestionsNum);
            Console.WriteLine(connectQuestionNums_json);
            var scriptPath = "..\\SOTISProj\\PythonScripts\\test_wrapper.py";
            var start = new ProcessStartInfo
            {
                
                FileName = "C:\\Users\\Laptop\\anaconda3\\envs\\SOTIS\\python.exe",
                Arguments = $"{scriptPath} \"{testParameters.FieldName}\" \"{connectQuestionNums_json}\"  \"{testParameters.DefQuestionsNum}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            string errors;
            string TestJson;
            try
            {
                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        TestJson = reader.ReadToEnd();
                    }
                    errors = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(TestJson))
                    {
                        Debug.WriteLine($"Standard Output: {TestJson}");
                    }
                    if (!string.IsNullOrWhiteSpace(errors))
                    {
                        Debug.WriteLine($"Standard Error: {errors}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    return BadRequest($"Error running Python script: {errors}");
                }

                //call the service 
                Test test =  _testService.createTest(TestJson, testParameters.FieldName, testParameters.Name);

                return Ok(test);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("ExtractAndSave/{fileName}")]
        public IActionResult ExtractAndSave(string fileName)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            try
            {
                // Step 1: Parse PDF
                string parsedText = RunPythonScript("..\\SOTISProj\\PythonScripts\\parsePDF.py", filePath);
                if (parsedText == null) return BadRequest("Error parsing PDF.");
                _dataService.SavePDFContent(parsedText);

                // Step 2: Extract Terms
                string termsResult = RunPythonScript("..\\SOTISProj\\PythonScripts\\first.py", $"\"{_dataService.GetPDFContent()}\" \"{_dataService.GetAcmSubTree()}\"");
                if (termsResult == null) 
                    return BadRequest("Error extracting terms.");

                ProcessTermsResult(termsResult, fileName);

                // Step 3: Get ACM Part
                string acmResult = RunPythonScript("..\\SOTISProj\\PythonScripts\\database_test.py", "\"Networks\"");
                if (acmResult == null) 
                    return BadRequest("Error retrieving ACM part.");
                _dataService.SaveAcmSubTree(acmResult.Split("###")[1]);

                // Step 4: Find New Relations
                string terms = string.Join(", ", _dataService.GetTermsRelations().Select(t => t.Key));
                string relationsResult = RunPythonScript("..\\SOTISProj\\PythonScripts\\second.py", $"\"{terms}\" \"{_dataService.GetAcmSubTree()}\" \"{_dataService.GetTermsRelations()}\"");
                if (relationsResult == null) 
                    return BadRequest("Error finding new relations.");

                // Step 5: Add Relations
                string finalResult = RunPythonScript("..\\SOTISProj\\PythonScripts\\third.py", $"\"{terms}\" \"{_dataService.GetTermsRelationsPairs()}\" \"{_dataService.GetTermsDefinitionsPairs()}\"");
                if (finalResult == null) 
                    return BadRequest("Error adding relations.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
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
        private void ProcessTermsResult(string termsResult, string fileName)
        {
            string jsonString1 = termsResult.Split("###")[0].Replace('\'', '"');
            string jsonString2 = termsResult.Split("###")[1].Replace('\'', '"');

            var definitions = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString1);

            var unwantedStrings = _context.termPdfs.Select(obj => obj.Term).ToList();

            var filteredDefinitions = definitions
                .Where(kvp => !unwantedStrings.Contains(kvp.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            

            var relations = JsonSerializer.Deserialize<Dictionary<string, RelatedTerms>>(jsonString2, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var filteredRelations = relations
                .Where(kvp => !unwantedStrings.Contains(kvp.Key)) // Exclude entire key-value pairs where the key is unwanted
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => {
                        kvp.Value.Related_to = kvp.Value.Related_to.Where(value => !unwantedStrings.Contains(value)).ToList();
                        return kvp.Value;
                    }
                );


            _dataService.SaveTermsDefinitions(filteredDefinitions);

            foreach (var item in filteredDefinitions)
            {
                Console.WriteLine($"{item.Key}: {item.Value}\n");
                TermPdf termPdf = new TermPdf();
                termPdf.Pdf = fileName;
                termPdf.Term = item.Key;
                _context.termPdfs.Add(termPdf);
                _context.SaveChanges();
            }

            _dataService.SaveTermsRelations(filteredRelations);

            foreach (var item in filteredRelations)
            {
                Console.WriteLine($"{item.Key}:");
                foreach (var relatedTerm in item.Value.Related_to)
                {
                    Console.WriteLine($"  - {relatedTerm}");
                }
                Console.WriteLine();
            }
        }
    }
}

