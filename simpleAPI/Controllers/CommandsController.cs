using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;
using SimpleAPI.Data;
using AutoMapper;
using SimpleAPI.DataTransferObjects;
using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace SimpleAPI.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandSource source;
        private readonly IMapper mapper;
        private readonly ILogger<CommandsController> logger;

        public CommandsController(ICommandSource source, IMapper mapper, ILogger<CommandsController> logger)
        {
            this.source = source;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            logger.LogDebug("'{0}' has been invoked", nameof(GetAllCommands));

            var dto = mapper.Map<IEnumerable<CommandReadDto>>(source.GetAllCommands());
            return Ok(dto);

        }

        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            logger.LogDebug("'{0}' has been invoked with parameter {1}", nameof(GetCommandById), id);

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
            logger.LogDebug("'{0}' has been invoked with parameter {1}", nameof(AddCommand), command);

            var commandModel = mapper.Map<Command>(command);
            source.CreateNewCommand(commandModel);
            source.SaveChanges();

            var commandRead = mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandRead.Id}, commandRead);

        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdate)
        {
            logger.LogDebug("'{0}' has been invoked with parameter id ={1} and {2}", nameof(UpdateCommand), id, commandUpdate);

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
            logger.LogDebug("'{0}' has been invoked with parameter id={1} and {2}", nameof(PatchCommand), id, patch);

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
            logger.LogDebug("'{0}' has been invoked with parameter id={1}", nameof(DeleteCommand), id);

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