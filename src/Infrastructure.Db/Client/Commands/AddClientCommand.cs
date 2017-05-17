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
            string databaseName = commandContext.DatabasePath;
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};",databaseName)))
            {
                conn.Open();
                SQLiteCommand verifyInnQuery =
                    new SQLiteCommand(
                        string.Format(
                            @"SELECT * FROM Clients WHERE Inn=@inn"), conn);
                verifyInnQuery.Parameters.AddWithValue("@inn", commandContext.Inn);
                SQLiteDataReader dataReader = verifyInnQuery.ExecuteReader();
                if (dataReader.HasRows)
                    throw new ArgumentException("Client with this INN already exists");

                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"INSERT INTO 'Clients' ('Name','INN') VALUES (@name, @inn);"), conn);
                command.Parameters.AddWithValue("@name", commandContext.Name);
                command.Parameters.AddWithValue("@inn", commandContext.Inn);
                command.ExecuteNonQuery();

                SQLiteCommand query =
                    new SQLiteCommand(
                        string.Format(
                            @"SELECT Id FROM Clients WHERE Inn=@inn"), conn);
                query.Parameters.AddWithValue("@inn", commandContext.Inn);
                int id=(int)query.ExecuteScalar();
                _clientService.AddClient(id, commandContext.Name, commandContext.Inn);
            }
        }
    }
}
