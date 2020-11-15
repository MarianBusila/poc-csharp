using AspNetCore31WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCore31WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilterController : ControllerBase
    {
        private readonly ILogger<FilterController> _logger;

        public FilterController(ILogger<FilterController> logger)
        {
            _logger = logger;
        }

        [HttpGet("testactionfilter")]
        [TimeElapsed]
        public string TestActionFilter()
        {
            return "Test time elapsed action filter.";
        }
    }
}
