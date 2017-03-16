using Domain.Commands.Contexts;

namespace Infrastructure.Db.Commands
{
    public class CommandBuilder : ICommandBuilder
    {
        private readonly ICommandFactory _factory;



        public CommandBuilder(ICommandFactory factory)
        {
            _factory = factory;
        }



        public void Execute<TCommandContext>(TCommandContext commandContext)
            where TCommandContext :  ICommandContext
        {
            _factory.Create<TCommandContext>().Execute(commandContext);
        }
    }
}
