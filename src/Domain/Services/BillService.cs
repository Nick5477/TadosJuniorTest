using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    class BillService:IBillService
    {
        private readonly IRepository<Bill> _repository;
        private readonly IClientService _clientService;

        public BillService(IRepository<Bill> repository,IClientService clientService)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            if (clientService==null)
                throw new ArgumentNullException(nameof(clientService));
            _repository = repository;
            _clientService = clientService;
        }
        public Bill AddBill(decimal sum, int clientId)
        {
            if (sum<0)
                throw new ArgumentException("Bill sum should be not negative!");
            if (_clientService.VerifyId(clientId) || clientId<=0)
                throw new NullReferenceException("Incorect client id!");
            DateTime createdAt=DateTime.UtcNow;
            Bill bill=new Bill(GetNewBillId(),sum,clientId,GetNewBillNumber(createdAt.Month),createdAt);
            _repository.Add(bill);
            return bill;
        }

        public void PayBill(int id,DateTime payedAt)
        {
            if (GetNewBillId()<=id)
                throw new NullReferenceException(nameof(Bill));
            _repository.All().SingleOrDefault(x=>x.Id==id).Pay(payedAt);
        }

        public List<Bill> GetBills(int offset, int count)
        {
            throw new NotImplementedException();
        }

        public int GetNewBillId()
        {
            return _repository.All().Count() + 1;
        }

        public int GetNewBillNumber(int month)
        {
            //_repository.All().SkipWhile(x=>x.DisplayNumber)
        }
    }
}
