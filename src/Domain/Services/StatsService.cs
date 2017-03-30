using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Domain.Structures;

namespace Domain.Services
{
    public class StatsService:IStatsService
    {
        private readonly IClientService _clientService;
        private readonly IRepository<Bill> _billRepository;
        private readonly IBillService _billService;

        public StatsService(IClientService clientService,
            IRepository<Bill> billRepository,
            IBillService billService)
        {
            if (clientService==null)
                throw new ArgumentNullException(nameof(clientService));
            if (billRepository==null)
                throw new ArgumentNullException(nameof(billRepository));
            if (billService==null)
                throw new ArgumentNullException(nameof(billService));
            _clientService = clientService;
            _billRepository = billRepository;
            _billService = billService;
        }
        //тестить OK
        public List<ClientPayedBillsSum> GetPayedBillsSum(int count, string startDateTime, string endDateTime)
        {
            if (count < 0)
                count = 0;
            if (count > 100)
                count = 100;
            if (!_billService.VerifyDateTime(startDateTime))
                throw new ArgumentException("Incorrect start datetime");
            if (!_billService.VerifyDateTime(endDateTime))
                throw new ArgumentException("Incorrect end datetime");
            if (startDateTime.CompareTo(endDateTime) > 0 && startDateTime != "" && endDateTime != "")
                throw new ArgumentException("Start datetime after end datetime!");
            List<ClientPayedBillsSum> billsSumList = new List<ClientPayedBillsSum>();
            List<Bill> bills=new List<Bill>();

            if (startDateTime != "" && endDateTime != "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill
                            .CreatedAt
                            .ToString("s")
                            .CompareTo(startDateTime) >= 0
                            && 
                            bill
                            .CreatedAt
                            .ToString("s")
                            .CompareTo(endDateTime) <= 0
                    )
                    .ToList();
            }
            else if (startDateTime == "" && endDateTime != "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                           bill
                           .CreatedAt
                           .ToString("s")
                           .CompareTo(endDateTime) <= 0
                    )
                    .ToList();
            }
            else if (startDateTime != "" && endDateTime == "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill
                            .CreatedAt
                            .ToString("s")
                            .CompareTo(startDateTime) >= 0
                    )
                    .ToList();
            }
            else
            {
                bills = _billRepository.All().ToList();
            }

            SortedSet<int> usedClientId=new SortedSet<int>();
            foreach (Bill bill in bills)
            {
                if (!usedClientId.Contains(bill.ClientId))
                {
                    ClientPayedBillsSum clientPayedBills = new ClientPayedBillsSum();
                    clientPayedBills.Sum = 
                        bills
                        .Where(b => b.ClientId == bill.ClientId)
                        .Sum(b => b.Sum);
                    clientPayedBills.Client = _clientService.GetClientById(bill.ClientId);
                    billsSumList.Add(clientPayedBills);
                    usedClientId.Add(bill.ClientId);
                }
            }
            billsSumList.Sort((b1, b2) => -b1.Sum.CompareTo(b2.Sum));
            return billsSumList.Take(count).ToList();
        }
        //тестить OK
        public BillsStat GetClientBillsStat(int id, string startDateTime, string endDateTime)
        {
            if (!_clientService.VerifyId(id))
                throw new ArgumentException("No client with this id");
            if (!_billService.VerifyDateTime(startDateTime))
                throw new ArgumentException("Incorrect start datetime");
            if (!_billService.VerifyDateTime(endDateTime))
                throw new ArgumentException("Incorrect end datetime");
            if (startDateTime.CompareTo(endDateTime) > 0 && startDateTime!="" && endDateTime!="")
                throw new ArgumentException("Start datetime after end datetime!");
            List<Bill> bills = new List<Bill>();
            if (startDateTime != "" && endDateTime != "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill
                            .CreatedAt
                            .ToString("s")
                            .CompareTo(startDateTime) >= 0
                            && 
                            bill
                            .CreatedAt
                            .ToString("s")
                            .CompareTo(endDateTime) <= 0
                            && 
                            bill.ClientId==id
                    )
                    .ToList();
            }
            else if (startDateTime == "" && endDateTime != "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                           bill
                           .CreatedAt
                           .ToString("s")
                           .CompareTo(endDateTime) <= 0
                           &&
                           bill.ClientId == id
                    )
                    .ToList();
            }
            else if (startDateTime != "" && endDateTime == "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill
                            .CreatedAt
                            .ToString("s")
                            .CompareTo(startDateTime) >= 0
                            &&
                            bill.ClientId == id
                    )
                    .ToList();
            }
            else
            {
                bills = 
                    _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill.ClientId == id
                    )
                    .ToList();
            }
            BillsStat clientBillsStat=new BillsStat();
            clientBillsStat.TotalCount = bills.Count;
            clientBillsStat.PayedCount = bills.Count(bill => bill.WasPayed);
            clientBillsStat.UnpayedCount = clientBillsStat.TotalCount - clientBillsStat.PayedCount;
            clientBillsStat.TotalSum = bills.Sum(bill => bill.Sum);
            clientBillsStat.PayedSum = bills.Where(bill => bill.WasPayed).Sum(bill => bill.Sum);
            clientBillsStat.UnpayedSum = clientBillsStat.TotalSum - clientBillsStat.PayedSum;
            return clientBillsStat;
        }
        //тестить OK
        public BillsStat GetAllBillsStat()
        {
            BillsStat billsStat=new BillsStat();
            billsStat.TotalCount = _billRepository.All().Count();
            billsStat.PayedCount = _billRepository.All().Count(bill => bill.WasPayed);
            billsStat.PayedSum = _billRepository.All().Where(bill => bill.WasPayed).Sum(bill => bill.Sum);
            billsStat.TotalSum = _billRepository.All().Sum(bill => bill.Sum);
            billsStat.UnpayedCount = billsStat.TotalCount - billsStat.PayedCount;
            billsStat.UnpayedSum = billsStat.TotalSum - billsStat.PayedSum;
            return billsStat;
        }
    }
}
