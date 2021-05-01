using System.Collections.Generic;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public interface ICommandSource
    {
            IEnumerable<Command> GetAllCommands();
            Command GetCommandById(int id);

    }
}