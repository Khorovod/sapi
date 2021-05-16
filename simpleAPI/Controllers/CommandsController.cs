using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;
using SimpleAPI.Data;
using AutoMapper;
using SimpleAPI.DataTransferObjects;
using System;
using Microsoft.AspNetCore.JsonPatch;

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
            var dto = mapper.Map<IEnumerable<CommandReadDto>>(source.GetAllCommands());
            return Ok(dto);
        }

        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var item = source.GetCommandById(id);
            if(item != null)
            {
                return Ok(mapper.Map<CommandReadDto>(item));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> AddCommand(CommandCreateDto command)
        {
            try
            {
                var commandModel = mapper.Map<Command>(command);
                source.CreateNewCommand(commandModel);
                source.SaveChanges();

                var commandRead = mapper.Map<CommandReadDto>(commandModel);
                return CreatedAtRoute(nameof(GetCommandById), new {Id = commandRead.Id}, commandRead);

            }
            catch(ArgumentNullException message)
            {
                return BadRequest(message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdate)
        {
            var modelFromSource = source.GetCommandById(id);
            if(modelFromSource == null)
            {
                return NotFound();
            }

            //когда две объекта с данными. Дбконтекст отслеживает изменения объектов, надо только сохранить
            mapper.Map(commandUpdate, modelFromSource);

            source.UpdateCommand(modelFromSource);
            source.SaveChanges();

            return NoContent();        
        }

        [HttpPatch("{id}")]
        public ActionResult PatchCommand(int id, JsonPatchDocument<CommandUpdateDto> patch)
        {
            var modelFromSource = source.GetCommandById(id);
            if(modelFromSource == null)
            {
                return NotFound();
            }

            var commandToPatch = mapper.Map<CommandUpdateDto>(modelFromSource);
            patch.ApplyTo(commandToPatch, ModelState);
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(commandToPatch, modelFromSource);

            source.UpdateCommand(modelFromSource);
            source.SaveChanges();

            return NoContent();  
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var modelFromSource = source.GetCommandById(id);
            if(modelFromSource == null)
            {
                return NotFound();
            }

            source.DeleteCommand(modelFromSource);
            source.SaveChanges();
            return NoContent();
        }
    }
}