using System.Collections.Generic;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public class DummyCommandRepository : ICommandSource
    {
        public IEnumerable<Command> GetAllCommands()
        {
            return new List<Command>
            {
                new Command{Id = 1, HowTo = "Bombo Clah", Line = "BOMBOCLAA", Platform = "Any"},
                new Command{Id = 2, HowTo = "RickRoll", Line = "Never give ones up", Platform = "Any"},
                new Command{Id = 3, HowTo = "Do a flip", Line = "Do a barrel roll", Platform = "Any"},
            };
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id = 1, HowTo = "Bombo Clah", Line = "BOMBOCLAA", Platform = "Any"};
        }
    }
}