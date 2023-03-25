using Microsoft.AspNetCore.Mvc;
using Module.Module1.Shared;

namespace Module.Module2
{
    [Route("[module]/[controller]")]
    internal class TestController : Controller
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Hello World from TestController in Module 2";
        }

        [HttpGet("InterModule")]
        public ActionResult<string> InterModule([FromServices] ITestService testService)
        {
            return $"{testService.SayHello()} in TestController in Module 2";
        }
    }
}