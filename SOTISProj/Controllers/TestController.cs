using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOTISProj.Repo;
using SOTISProj.SeriveInterfaces;
using SOTISProj.Services;

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
    }
}
