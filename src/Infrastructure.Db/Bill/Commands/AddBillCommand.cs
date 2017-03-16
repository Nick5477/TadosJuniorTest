using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Contexts;
using Infrastructure.Db.Commands;

namespace Infrastructure.Db.Bill.Commands
{
    class AddBillCommand:ICommand<AddBillCommandContext>
    {
        public void Execute(AddBillCommandContext commandContext)
        {
            string databaseName = "database.db";
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"INSERT INTO 'Bills' ('Id','Sum','Number','ClientId','CreatedAt') VALUES (@id, @sum, @number,@clid,@createdat);"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.Parameters.AddWithValue("@sum", (int)commandContext.Sum);
                command.Parameters.AddWithValue("@number", commandContext.Number);
                command.Parameters.AddWithValue("@clid", commandContext.ClientId);
                command.Parameters.AddWithValue("@createdat", commandContext.CreatedAt.ToString("s"));
                command.ExecuteNonQuery();
            }
        }
    }
}
