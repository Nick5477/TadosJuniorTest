using System;
using System.Data.SQLite;
using Domain.Commands.Contexts;
using Domain.Services;
using Infrastructure.Db.Commands;

namespace Infrastructure.Db.Bill.Commands
{
    class AddBillCommand:ICommand<AddBillCommandContext>
    {
        private readonly IBillService _billService;

        public AddBillCommand(IBillService billService)
        {
            if (billService==null)
                throw new ArgumentNullException(nameof(billService));
            _billService = billService;
        }
        public void Execute(AddBillCommandContext commandContext)
        {
            Domain.Entities.Bill bill=_billService.AddBill(commandContext.Sum, commandContext.ClientId);

            string databaseName =
                @"C:\Users\User\Documents\Visual Studio 2015\Projects\TadosJuniorTest\src\WebApp\bin\database.db";
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"INSERT INTO 'Bills' ('Id','Sum','Number','ClientId','CreatedAt') VALUES (@id, @sum, @number,@clid,@createdat);"), conn);
                command.Parameters.AddWithValue("@id", bill.Id);
                command.Parameters.AddWithValue("@sum", (int)bill.Sum);
                command.Parameters.AddWithValue("@number", bill.Number);
                command.Parameters.AddWithValue("@clid", bill.ClientId);
                command.Parameters.AddWithValue("@createdat", bill.CreatedAt.ToString("s"));
                command.ExecuteNonQuery();
            }
        }
    }
}
