using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using System.Text.RegularExpressions;

namespace Domain.Services
{
    public class BillService:IBillService
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
        public Bill AddBill(int id, int number, decimal sum, int clientId,DateTime createdAt)
        {
            if (!_clientService.VerifyId(clientId) || clientId<=0)
                throw new NullReferenceException("Incorect client id!");
            Bill bill=new Bill(
                id,
                sum,
                clientId,
                number,
                createdAt);
            _repository.Add(bill);
            return bill;
        }
        public bool PayBill(int id,DateTime payedAt)
        {
            if (_repository.All().All(bill => bill.Id != id))//если такого счета нет
                return false;
            _repository.All().SingleOrDefault(bill=>bill.Id==id).Pay(payedAt);
            return true;
        }
        public List<Bill> GetBills(int offset, int count)
        {
            if (offset < 0)
                offset = 0;
            if (count < 0)
                count = 0;
            if (count > 100)
                count = 100;
            List<Bill> bills = _repository
                .All()
                .SkipWhile(
                bill => bill.Id < offset
                )
                .Take(count)
                .ToList();
            bills
                .Sort(
                (bill1, bill2)=>
                        bill1.GetReverseDisplayNumber().CompareTo(bill2.GetReverseDisplayNumber()));
            return bills;
        }
        public List<Bill> GetClientBills(int id, int offset, int count)
        {
            if (offset < 0)
                offset = 0;
            if (count < 0)
                count = 0;
            if (count > 100)
                count = 100;
            List<Bill> bills =
                _repository
                    .All()
                    .Where(
                        bill => bill.ClientId == id
                    )
                    .Skip(offset)
                    .Take(count)
                    .ToList();
            bills
                .Sort(
                (bill1, bill2) =>
                bill1.GetReverseDisplayNumber().CompareTo(bill2.GetReverseDisplayNumber()));
            return bills;
        }
        public DateTime StringToDateTime(string date)
        {
            DateTime dateTime=
                new DateTime(
                    int.Parse(date.Substring(0, 4)),
                    int.Parse(date.Substring(5,2)),
                    int.Parse(date.Substring(8,2)),
                    int.Parse(date.Substring(11,2)),
                    int.Parse(date.Substring(14,2)),
                    int.Parse(date.Substring(17)));
            return dateTime;
        }
    }
}
