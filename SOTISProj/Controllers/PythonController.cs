using Microsoft.AspNetCore.Mvc;
using SOTISProj.Services;
using System.Diagnostics;

namespace SOTISProj.Controllers
{
    [Route("api/python")]
    [ApiController]
    public class PythonController : ControllerBase
    {
        private readonly IDataService _dataService;
        public PythonController(IDataService dataService)
        {
            _dataService = dataService;
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
                    errors = process.StandardError.ReadToEnd(); // Capture errors

                    // Log output and errors for debugging
                    if (!string.IsNullOrWhiteSpace(ParsedText))
                    {
                        Debug.WriteLine($"Standard Output: {ParsedText}");
                        //Console.WriteLine($"Text: {ParsedText}");
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
                RedirectStandardError = true, // Capture standard error
                UseShellExecute = false,
                CreateNoWindow = true
            };
            //Console.WriteLine($"TEKSTCINA: {_dataService.GetPDFContent()}");
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
                    errors = process.StandardError.ReadToEnd(); // Capture errors

                    // Log output and errors for debugging
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

        [HttpGet("FindDefinitions")]
        public IActionResult RunTermsDefinitions()
        {
            var scriptPath = "..\\SOTISProj\\PythonScripts\\second.py";
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe",
                Arguments = $"{scriptPath} \"{_dataService.GetTermsRelations()}\" \"{_dataService.GetPDFContent()}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true, // Capture standard error
                UseShellExecute = false,
                CreateNoWindow = true
            };
            //Console.WriteLine($"TEKSTCINA: {_dataService.GetPDFContent()}");
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
                    errors = process.StandardError.ReadToEnd(); // Capture errors

                    // Log output and errors for debugging
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
                RedirectStandardError = true, // Capture standard error
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
                        _dataService.SaveAcmSubTree(result);
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

    }
}
