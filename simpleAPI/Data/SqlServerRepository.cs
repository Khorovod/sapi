using System;
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

        public void CreateNewCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            context.Commands.Add(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return context.Commands.FirstOrDefault(l => l.Id == id);
        }

        public bool SaveChanges()
        {
            //сейв у всего контекста
            return context.SaveChanges() >=0;
        }

        public void UpdateCommand(Command command)
        {
            //тут ничего не делаем, спасибо эф. Просто мапим данные в контроллере
        }
    }
}