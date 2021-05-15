using System.Collections.Generic;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public interface ICommandSource
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateNewCommand(Command command);
        bool SaveChanges();
        void UpdateCommand(Command command);
    }
}