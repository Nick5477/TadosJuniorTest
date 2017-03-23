using System.Collections.Generic;
using System.Data.SQLite;
using Domain.Queries.Criteria;
using Infrastructure.Db.Queries;

namespace Infrastructure.Db.Client.Queries
{
    public class GetAllClientsFromDbQuery:IQuery<EmptyCriterion,IEnumerable<Domain.Entities.Client>>
    {
        public IEnumerable<Domain.Entities.Client> Ask(EmptyCriterion criterion)
        {
            string databaseName = "database.db";
            List<Domain.Entities.Client> clients=new List<Domain.Entities.Client>();
            using (SQLiteConnection conn = new SQLiteConnection(string.Format(@"Data Source={0};", databaseName)))
            {
                conn.Open();
                SQLiteCommand command =
                    new SQLiteCommand(
                        string.Format(
                            @"SELECT * FROM Clients"), conn);
                SQLiteDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Domain.Entities.Client client=
                        new Domain.Entities.Client(
                        dataReader.GetInt32(0),
                        dataReader.GetString(1),
                        dataReader.GetString(2));
                    clients.Add(client);
                }
            }
            return clients;
        }
    }
}
