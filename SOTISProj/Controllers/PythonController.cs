using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SOTISProj.Controllers
{
    [Route("api/python")]
    [ApiController]
    public class PythonController : ControllerBase
    {
        [HttpGet]
        public IActionResult RunPythonScript()
        {
            var start = new ProcessStartInfo
            {
                FileName = "C:\\Users\\bosko\\Desktop\\SOTIS\\okruzenje\\Scripts\\python.exe", // Absolute path to the Python interpreter in virtualenv
                Arguments = "..\\SOTISProj\\PythonScripts\\proba.py", // Path to the Python script
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
