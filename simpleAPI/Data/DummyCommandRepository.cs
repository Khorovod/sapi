using System.Collections.Generic;
using SimpleAPI.Models;
using System;
using System.Linq;

namespace SimpleAPI.Data
{
    public class DummyCommandRepository : ICommandSource
    {
        List<Command> mock = new List<Command>
        {
            new Command{Id = 1, HowTo = "Bombo Clah", Line = "BOMBOCLAA", Platform = "Any"},
            new Command{Id = 2, HowTo = "RickRoll", Line = "Never give ones up", Platform = "Any"},
            new Command{Id = 3, HowTo = "Do a flip", Line = "Do a barrel roll", Platform = ""},
        };
            

        public void CreateNewCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException();
            }
            mock.Add(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return mock;
        }

        public Command GetCommandById(int id)
        {
            return id == 1 ? new Command{Id = id, HowTo = "Bombo Clah", Line = "BOMBOCLAA", Platform = "Any"} : null;
        }

        public bool SaveChanges() => mock.Count > 3 ? true : false;

        public void UpdateCommand(Command command)
        {
            var commandToUpdate = mock.Single(x => x.Id == command.Id);
            if(commandToUpdate != null)
            {
                commandToUpdate.HowTo = command.HowTo;
                commandToUpdate.Line = command.Line;
                //hmmmmm
                if(command.Platform == null)
                {
                    commandToUpdate.Platform = "";
                }
                else
                {
                    commandToUpdate.Platform = command.Platform;
                }
            }
        }
    }
}