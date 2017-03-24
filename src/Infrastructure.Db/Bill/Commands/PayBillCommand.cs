using System;
using System.Data.SQLite;
using Domain.Commands.Contexts;
using Domain.Services;
using Infrastructure.Db.Commands;

namespace Infrastructure.Db.Bill.Commands
{
    class PayBillCommand:ICommand<PayBillCommandContext>
    {
        private readonly IBillService _billService;

        public PayBillCommand(IBillService billService)
        {
            if (billService == null)
                throw new ArgumentNullException(nameof(billService));
            _billService = billService;
        }
        public void Execute(PayBillCommandContext commandContext)
        {
            DateTime payedDateTime = DateTime.UtcNow;
            _billService.PayBill(commandContext.Id,payedDateTime);

            string databaseName =
                @"C:\Users\User\Documents\Visual Studio 2015\Projects\TadosJuniorTest\src\WebApp\bin\database.db";
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"UPDATE Bills SET PayedAt=@payedat WHERE Id=@id;"), conn);
                command.Parameters.AddWithValue("@id", commandContext.Id);
                command.Parameters.AddWithValue("@payedat", payedDateTime.ToString("s"));
                command.ExecuteNonQuery();
            }
        }
    }
}
