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
        public Bill AddBill(decimal sum, int clientId)
        {
            if (sum<0)
                throw new ArgumentException("Bill sum should be not negative!");
            if (!_clientService.VerifyId(clientId) || clientId<=0)
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
        public bool PayBill(int id,DateTime payedAt)
        {
            if (GetNewBillId() <= id)
                return false;
            _repository.All().SingleOrDefault(bill=>bill.Id==id).Pay(payedAt);
            return true;
        }
        //тестить OK
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
            if (!_repository.All().Any())
                return 1;
            return _repository.All().Count() + 1;
        }
        //тестить OK
        public int GetNewBillNumber(int month,int year)
        {
            string mm = month.ToString("00");
            string yyyy = year.ToString("0000");
            string maxDisplayNumber = _repository.All()
                .Where(
                bill => bill.DisplayNumber.StartsWith(mm+"."+yyyy)
                )
                .Max(bill=>bill.DisplayNumber);
            if (maxDisplayNumber == null)
                return 1;
            return int.Parse(maxDisplayNumber.Substring(maxDisplayNumber.IndexOf("-")+1)) + 1;
        }
        //тестить OK
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
            string year = displayNumber.Substring(3, 4);
            displayNumber = displayNumber.Remove(2, 5);
            displayNumber = displayNumber.Insert(0, year + ".");
            return displayNumber;
        }
        //OK
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
        public bool VerifyDateTime(string date)
        {
            if (date == "")
                return true;
            return Regex.IsMatch(date, @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}");
        }
    }
}
