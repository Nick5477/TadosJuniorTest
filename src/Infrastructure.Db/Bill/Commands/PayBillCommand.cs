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
    class PayBillCommand:ICommand<PayBillCommandContext>
    {
        public void Execute(PayBillCommandContext commandContext)
        {
            string databaseName = "database.db";
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"UPDATE Bills SET PayedAt=@payedat WHERE Id=@id;"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.Parameters.AddWithValue("@payedat", DateTime.UtcNow.ToString("s"));
                command.ExecuteNonQuery();
            }
        }
    }
}
