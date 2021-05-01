using System.Collections.Generic;
using System.Linq;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public class SqlServerRepository : ICommandSource
    {
        private readonly CommandContext context;

        public SqlServerRepository(CommandContext context)
        {
            this.context = context;
        }
        public IEnumerable<Command> GetAllCommands()
        {
            return context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return context.Commands.FirstOrDefault(l => l.Id == id);
        }
    }
}