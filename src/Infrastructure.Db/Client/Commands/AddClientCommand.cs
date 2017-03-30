using System;
using Domain.Commands.Contexts;
using System.Data.SQLite;
using Infrastructure.Db.Commands;
using Domain.Services;

namespace Infrastructure.Db.Client.Commands
{
    public class AddClientCommand : ICommand<AddClientCommandContext>
    {
        private readonly IClientService _clientService;

        public AddClientCommand(IClientService clientService)
        {
            if (clientService==null)
                throw new ArgumentNullException(nameof(clientService));
            _clientService = clientService;
        }
        public void Execute(AddClientCommandContext commandContext)
        {
            Domain.Entities.Client client=_clientService.AddClient(commandContext.Name, commandContext.Inn);

            string databaseName = commandContext.DatabasePath;
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};",databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"INSERT INTO 'Clients' ('Id','Name','INN') VALUES (@id, @name, @inn);"), conn);
                command.Parameters.AddWithValue("@id", client.Id);
                command.Parameters.AddWithValue("@name", client.Name);
                command.Parameters.AddWithValue("@inn", client.Inn);
                command.ExecuteNonQuery();
            }
        }
    }
}
