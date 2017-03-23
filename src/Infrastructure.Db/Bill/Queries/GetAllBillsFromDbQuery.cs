using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Domain.Queries.Criteria;
using Domain.Services;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Bill.Queries
{
    public class GetAllBillsFromDbQuery:IQuery<EmptyCriterion,IEnumerable<Domain.Entities.Bill>>
    {
        private readonly IBillService _billService;

        public GetAllBillsFromDbQuery(IBillService billService)
        {
            if (billService==null)
                throw new ArgumentNullException(nameof(billService)); 
            _billService = billService;
        }
        public IEnumerable<Domain.Entities.Bill> Ask(EmptyCriterion criterion)
        {
            string databaseName = "database.db";
            List<Domain.Entities.Bill> bills = new List<Domain.Entities.Bill>();
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"SELECT * FROM Bills"), conn);
                SQLiteDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Domain.Entities.Bill bill =
                        new Domain.Entities.Bill(
                        dataReader.GetInt32(0),
                        dataReader.GetDecimal(1),
                        dataReader.GetInt32(3),
                        dataReader.GetInt32(2),
                        _billService.StringToDateTime(dataReader.GetString(4)));
                    if (!dataReader.IsDBNull(5))
                    {
                        DateTime payedAt = _billService.StringToDateTime(dataReader.GetString(5));
                        bill.Pay(payedAt);
                    }
                    bills.Add(bill);
                }
            }
            return bills;
        }
    }
}
