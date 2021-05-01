using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;
using SimpleAPI.Data;

namespace SimpleAPI.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandSource source;

        public CommandsController(ICommandSource source)
        {
            this.source = source;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            return Ok(source.GetAllCommands());
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var item = source.GetCommandById(id);
            if(item != null)
            {
                return Ok();
            }
            return NotFound();
        }

    }
}