using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Contexts;
using Domain.Entities;
using Domain.Queries.Criteria;
using Domain.Repositories;
using Infrastructure.Db.Commands;
using Infrastructure.Db.Queries;

namespace ConsoleApp
{
    class App
    {
        private readonly ICommandBuilder _commandBuilder;
        private readonly IQueryBuilder _queryBuilder;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Bill> _billRepository;

        public App(
            ICommandBuilder commandBuilder,
            IQueryBuilder queryBuilder,
            IRepository<Client> clientRepository,
            IRepository<Bill> billRepository)
        {
            _commandBuilder = commandBuilder;
            _queryBuilder = queryBuilder;
            _clientRepository = clientRepository;
            _billRepository = billRepository;
        }

        public void Start1()
        {
            var clients = _queryBuilder.For<IEnumerable<Client>>().With(new EmptyCriterion());
            foreach (Client client in clients)
            {
                _clientRepository.Add(client);
            }
            var bills = _queryBuilder.For<IEnumerable<Bill>>().With(new EmptyCriterion());
            foreach (Bill bill in bills)
            {
                _billRepository.Add(bill);
            }
        }
        public void AddNewClient(string name,string inn)
        {
            _commandBuilder.Execute(new AddClientCommandContext()
            {
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

        public void AddNewBill(int clientId, decimal sum)
        {
            _commandBuilder.Execute(new AddBillCommandContext()
            {
                ClientId = clientId,
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

        public IEnumerable<Bill> GetBills(int offset, int count)
        {
            return _queryBuilder
                .For<IEnumerable<Bill>>()
                .With(new GetBillsCriterion()
                {
                    Offset = offset,
                    Count=count
                });
        }

        public IEnumerable<Bill> GetClientBills(int id, int offset, int count)
        {
            return _queryBuilder
                .For<IEnumerable<Bill>>()
                .With(new GetClientBillsCriterion()
                {
                    Id = id,
                    Offset = offset,
                    Count = count
                });
        }
    }
}
