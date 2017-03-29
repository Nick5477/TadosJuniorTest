using System;
using System.Data.SQLite;
using Domain.Commands.Contexts;
using Domain.Services;
using Infrastructure.Db.Commands;

namespace Infrastructure.Db.Client.Commands
{
    public class ChangeClientNameCommand:ICommand<ChangeClientNameCommandContext>
    {
        private readonly IClientService _clientService;

        public ChangeClientNameCommand(IClientService clientService)
        {
            if (clientService==null)
                throw new ArgumentNullException(nameof(clientService));
            _clientService = clientService;
        }
        public void Execute(ChangeClientNameCommandContext commandContext)
        {
            Domain.Entities.Client client=_clientService.GetClientById(commandContext.Id);
            _clientService.ChangeClientName(client,commandContext.Name);

            string databaseName =
                @"C:\Users\User\Documents\Visual Studio 2015\Projects\TadosJuniorTest\src\WebApp\bin\database.db";
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"UPDATE Clients SET Name=@newname where Id=@id"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.Parameters.AddWithValue("@newname", commandContext.Name);
                command.ExecuteNonQuery();
            }
        }
    }
}
