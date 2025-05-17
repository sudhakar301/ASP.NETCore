using Microsoft.AspNetCore.Mvc;

namespace SampleASPDotNetCore.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("item")]
        public IActionResult Index()
        {
            return Ok("string");
        }
    }
}
