using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")] // because of api gateway, we need a different path than in platform service
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Servie");
            return Ok("Inbound test of platforms controller");
        }
    }
}