using NUnit.Framework;
using simpleAPI.UnitTests.MockData;
using SimpleAPI.Controllers;
using AutoMapper;
using SimpleAPI.Models;
using SimpleAPI.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace simpleAPI.UnitTests
{
    public class ControllerTests
    {
        private Mapper _mapper;
        private CommandsController _testController;

        [SetUp]
        public void Setup()
        {
            var sourse = new DummyCommandRepository();
            _mapper = new Mapper(new MapperConfiguration(conf => 
            {
                conf.CreateMap<Command, CommandReadDto>();
                conf.CreateMap<CommandCreateDto, Command>();
                conf.CreateMap<CommandUpdateDto, Command>().ReverseMap();
            }));
            _testController = new CommandsController(sourse, _mapper, null);
        }

        [Test]
        public void DeleteExistReturn204()
        {
            var delete = _testController.DeleteCommand(2);

            var noCont = delete as NoContentResult;
            Assert.AreEqual(StatusCodes.Status204NoContent, noCont.StatusCode);
        }

        [Test]
        public void DeleteNoExistReturn404()
        {
            var delete = _testController.DeleteCommand(10);
            
            var notFound = delete as NotFoundResult;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFound.StatusCode);
        }

        [Test]
        public void PatchNonExistReturn404()
        {
            var patch = _testController.PatchCommand(10, null);

            var notFound = patch as NotFoundResult;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFound.StatusCode);
        }

        [Test]
        public void GetAllReturn200()
        {
            var get = _testController.GetAllCommands().Result;

            var ok = get as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, ok.StatusCode);

        }

        [Test]
        public void GetAllReturnCorrectItem()
        {
            var get = _testController.GetAllCommands().Result;

            var ok = get as OkObjectResult;
            var okValue = (List<CommandReadDto>)ok.Value;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(StatusCodes.Status200OK, ok.StatusCode);
                Assert.IsTrue(okValue.First().Id == 1);
                Assert.IsTrue(okValue.Where(x => x.HowTo == "Bombo Clah").First().Id == 1);
                Assert.IsTrue(okValue.Where(x=> x.Line == "BOMBOCLAA").First().Id == 1);
            });

        }

        [Test]
        public void GetByIdReturnCorrectItem()
        {
            var get = _testController.GetCommandById(2).Result;

            var ok = get as OkObjectResult;
            var okValue = (CommandReadDto)ok.Value;


            Assert.Multiple(() =>
            {
                Assert.AreEqual(StatusCodes.Status200OK, ok.StatusCode);
                Assert.IsTrue(okValue.Id == 2);
                Assert.IsTrue(okValue.HowTo == "RickRoll");
                Assert.IsTrue(okValue.Line == "Never give ones up");
            });

        }

        [Test]
        public void AddItemReturn201()
        {
            var command = new CommandCreateDto();
            command.HowTo = "testHowTo";
            command.Line = "testLine";
            command.Platform = "testPlatform";

            var add = _testController.AddCommand(command).Result;

            var ok = add as CreatedAtRouteResult;
            var okValue = (CommandReadDto)ok.Value;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(StatusCodes.Status201Created, ok.StatusCode);
                Assert.AreEqual(command.HowTo, okValue.HowTo);
                Assert.AreEqual(command.Line, okValue.Line);
                Assert.AreEqual(okValue.Id, 4);
            });
        }
    }
}