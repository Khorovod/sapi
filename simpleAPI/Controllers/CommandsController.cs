using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;
using SimpleAPI.Data;
using AutoMapper;
using SimpleAPI.DataTransferObjects;

namespace SimpleAPI.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandSource source;
        private readonly IMapper mapper;

        public CommandsController(ICommandSource source, IMapper mapper)
        {
            this.source = source;
            this.mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var result = source.GetAllCommands();
            return Ok(mapper.Map<CommandReadDto>(result));
        }

        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var item = source.GetCommandById(id);
            if(item != null)
            {
                return Ok(mapper.Map<CommandReadDto>(item));
            }
            return NotFound();
        }

    }
}