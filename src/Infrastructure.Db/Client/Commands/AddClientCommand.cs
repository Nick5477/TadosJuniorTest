using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Contexts;
using System.Data.SQLite;
using Infrastructure.Db.Commands;
using System.Data.SQLite.Generic;

namespace Infrastructure.Db.Client.Commands
{
    public class AddClientCommand : ICommand<AddClientCommandContext>
    {
        public void Execute(AddClientCommandContext commandContext)
        {
            string databaseName = "database.db";
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};",databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"INSERT INTO 'Clients' ('Id','Name','INN') VALUES (@id, @name, @inn);"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.Parameters.AddWithValue("@name", commandContext.Name);
                command.Parameters.AddWithValue("@inn", commandContext.Inn);
                command.ExecuteNonQuery();
            }
        }
    }
}
