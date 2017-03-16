using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Contexts;
using Infrastructure.Db.Commands;

namespace Infrastructure.Db.Client.Commands
{
    class ChangeClientNameCommand:ICommand<ChangeClientNameCommandContext>
    {
        public void Execute(ChangeClientNameCommandContext commandContext)
        {
            string databaseName = "database.db";
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"UPDATE Clients SET Name=@newname where Id=@id"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.Parameters.AddWithValue("@newname", commandContext.NewName);
                command.ExecuteNonQuery();
            }
        }
    }
}
