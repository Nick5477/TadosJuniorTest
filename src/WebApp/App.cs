using System.Collections.Generic;
using Domain.Entities;
using Domain.Queries.Criteria;
using Domain.Repositories;
using Infrastructure.Db.Queries;

namespace WebApp
{
    public class App
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Bill> _billRepository;

        public App(
            IQueryBuilder queryBuilder,
            IRepository<Client> clientRepository,
            IRepository<Bill> billRepository)
        {
            _queryBuilder = queryBuilder;
            _clientRepository = clientRepository;
            _billRepository = billRepository;
        }

        public void StartInitialization()
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
       
    }
}
