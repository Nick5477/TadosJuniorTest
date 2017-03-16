using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Contexts;
using Infrastructure.Db.Commands;

namespace ConsoleApp
{
    class App
    {
        private readonly ICommandBuilder _commandBuilder;

        public App(ICommandBuilder commandBuilder)
        {
            _commandBuilder = commandBuilder;
        }

        public void AddNewClient(int id,string name,string inn)
        {
            _commandBuilder.Execute(new AddClientCommandContext()
            {
                Id = id,
                Name = name,
                Inn = inn
            });
        }

        public void ChangeClientName(int id, string newName)
        {
            _commandBuilder.Execute(new ChangeClientNameCommandContext()
            {
                Id=id,
                NewName = newName
            });
        }

        public void DeleteClient(int id)
        {
            _commandBuilder.Execute(new DeleteClientCommandContext()
            {
                Id=id
            });
        }

        public void AddNewBill(int id, int clientId, decimal sum, int number, DateTime createdAt)
        {
            _commandBuilder.Execute(new AddBillCommandContext()
            {
                Id=id,
                ClientId = clientId,
                Number = number,
                CreatedAt = createdAt,
                Sum=sum
            });
        }

        public void PayBill(int id)
        {
            _commandBuilder.Execute(new PayBillCommandContext()
            {
                Id=id
            });
        }
    }
}
