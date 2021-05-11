using System.Collections.Generic;
using SimpleAPI.Models;
using System;

namespace SimpleAPI.Data
{
    public class DummyCommandRepository : ICommandSource
    {
        List<Command> mock = new List<Command>
        {
            new Command{Id = 1, HowTo = "Bombo Clah", Line = "BOMBOCLAA", Platform = "Any"},
            new Command{Id = 2, HowTo = "RickRoll", Line = "Never give ones up", Platform = "Any"},
            new Command{Id = 3, HowTo = "Do a flip", Line = "Do a barrel roll", Platform = "Any"},
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

    }
}