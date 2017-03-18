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
            Bill bill=new Bill(
                GetNewBillId(),
                sum,
                clientId,
                GetNewBillNumber(createdAt.Month, createdAt.Year),
                createdAt);
            _repository.Add(bill);
            return bill;
        }
        public void PayBill(int id,DateTime payedAt)
        {
            if (GetNewBillId()<=id)
                throw new NullReferenceException(nameof(Bill));
            _repository.All().SingleOrDefault(bill=>bill.Id==id).Pay(payedAt);
        }
        //тестить
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
                BillsCompareByDisplayNumber(bill1.DisplayNumber, bill2.DisplayNumber));
            return bills;
        }
        
        public int GetNewBillId()
        {
            return _repository.All().Count() + 1;
        }
        //тестить
        public int GetNewBillNumber(int month,int year)
        {
            string mm = month.ToString("00");
            string yyyy = year.ToString("0000");
            string maxDisplayNumber = _repository.All()
                .Where(
                bill => bill.DisplayNumber.StartsWith(mm+"."+yyyy)
                )
                .Max(bill=>bill.DisplayNumber);
            return int.Parse(maxDisplayNumber) + 1;
        }
        //тестить
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
                BillsCompareByDisplayNumber(bill1.DisplayNumber, bill2.DisplayNumber));
            return bills;
        }
        private int BillsCompareByDisplayNumber(string billDisplayNumber1, string billDisplayNumber2)
        {
            billDisplayNumber1 = DisplayNumberMonthYearReverse(billDisplayNumber1);
            billDisplayNumber2 = DisplayNumberMonthYearReverse(billDisplayNumber2);
            return billDisplayNumber1.CompareTo(billDisplayNumber2);
        }
        private string DisplayNumberMonthYearReverse(string displayNumber)
        {
            string year = displayNumber.Substring(2, 4);
            displayNumber = displayNumber.Remove(1, 5);
            displayNumber = displayNumber.Insert(0, year + ".");
            return displayNumber;
        }
    }
}
