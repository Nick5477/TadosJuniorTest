using Domain.Commands.Contexts;

namespace Infrastructure.Db.Commands
{
    public interface ICommandFactory
    {
        ICommand<TCommandContext> Create<TCommandContext>()
            where TCommandContext : ICommandContext;
    }
}
