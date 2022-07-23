using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SimpleAPI.Extensions;
using SimpleAPI.Models;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly IFactory<ITime> _factory;

        public TimeController(IFactory<ITime> factory)
        {
            _factory = factory;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentTime()
        {
            var time = await Task.FromResult(_factory.Create().TimeNow());
            return Ok(time);
        }
    }
}
