using System;
using System.Data.SQLite;
using System.IO;
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

            string databaseName = commandContext.DatabasePath;
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();

                DateTime createdAt=DateTime.UtcNow;

                SQLiteCommand getNewBillNumberQuery=
                    new SQLiteCommand(
                        string.Format(
                            @"select COUNT(NUMBER) from bills where CreatedAt LIKE @yearMonth + '%' "), conn);
                getNewBillNumberQuery.Parameters.AddWithValue("@yearMonth",
                    $"{createdAt.Year:0000}-{createdAt.Month:00}");
                int number = (int)getNewBillNumberQuery.ExecuteScalar()+1;


                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"INSERT INTO 'Bills' ('Sum','Number','ClientId','CreatedAt') VALUES (@sum, @number,@clid,@createdat);"), conn);
                command.Parameters.AddWithValue("@sum", commandContext.Sum);
                command.Parameters.AddWithValue("@number", number);
                command.Parameters.AddWithValue("@clid", commandContext.ClientId);
                command.Parameters.AddWithValue("@createdat", createdAt.ToString("s"));
                command.ExecuteNonQuery();

                SQLiteCommand getNewBillIdQuery =
                    new SQLiteCommand(
                        string.Format(
                            @"SELECT Id FROM Bills WHERE Number=@number AND CreatedAt=@createdat), conn);"));
                getNewBillIdQuery.Parameters.AddWithValue("@number", number);
                getNewBillIdQuery.Parameters.AddWithValue("@createdat", createdAt.ToString("s"));
                int id = (int) getNewBillIdQuery.ExecuteScalar();

                _billService.AddBill(id,number, commandContext.Sum, commandContext.ClientId, createdAt);
            }
        }
    }
}
