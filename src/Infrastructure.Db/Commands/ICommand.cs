using Domain.Commands.Contexts;

namespace Infrastructure.Db.Commands
{
    public interface ICommand<in TCommandContext>
        where TCommandContext : ICommandContext
    {
        void Execute(TCommandContext commandContext);
    }
}
