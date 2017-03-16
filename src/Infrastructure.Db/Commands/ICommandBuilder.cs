using Domain.Commands.Contexts;

namespace Infrastructure.Db.Commands
{
    public interface ICommandBuilder
    {
        void Execute<TCommandContext>(TCommandContext commandContext)
            where TCommandContext : ICommandContext;
    }
}
