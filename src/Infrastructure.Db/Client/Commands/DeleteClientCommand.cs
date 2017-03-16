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
    class DeleteClientCommand:ICommand<DeleteClientCommandContext>
    {
        public void Execute(DeleteClientCommandContext commandContext)
        {
            string databaseName = "database.db";
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"DELETE FROM Bills WHERE ClientId=@id"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                command =
                    new SQLiteCommand(
                        string.Format(
                            @"DELETE FROM Clients WHERE Id=@id"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
