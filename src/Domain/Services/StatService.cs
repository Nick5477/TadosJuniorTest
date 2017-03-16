using System;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Domain.Structures;

namespace Domain.Services
{
    class StatService:IStatService
    {
        private readonly IBillService _billService;
        private readonly IClientService _clientService;
        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<Client> _clientRepository;

        public StatService(IBillService billService,
            IClientService clientService,
            IRepository<Bill> billRepository,
            IRepository<Client> clientRepository)
        {
            if (billService==null)
                throw new ArgumentNullException(nameof(billService));
            if (clientService==null)
                throw new ArgumentNullException(nameof(clientService));
            if (billRepository==null)
                throw new ArgumentNullException(nameof(billRepository));
            if (clientRepository == null)
                throw new ArgumentNullException(nameof(clientRepository));
            _billService = billService;
            _clientService = clientService;
            _billRepository = billRepository;
            _clientRepository = clientRepository;
        }
        public ClientPayedBillsSum GetPayedBillsSum(int count, string StartDateTime, string EndDateTime)
        {
            throw new NotImplementedException();
        }

        public ClientBillsStat GetClientBillsStat(int id, string StartDateTime, string EndDateTime)
        {
            throw new NotImplementedException();
        }

        public BillsStat GetAllBillsStat()
        {
            BillsStat billsStat=new BillsStat();
            billsStat.TotalCount = _billRepository.All().Count();
            int payedCount = 0;
            int payedSum = 0;
        }
    }
}
