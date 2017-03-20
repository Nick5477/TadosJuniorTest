using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Domain.Structures;

namespace Domain.Services
{
    class StatService:IStatService
    {
        private readonly IClientService _clientService;
        private readonly IRepository<Bill> _billRepository;

        public StatService(IClientService clientService,
            IRepository<Bill> billRepository)
        {
            if (clientService==null)
                throw new ArgumentNullException(nameof(clientService));
            if (billRepository==null)
                throw new ArgumentNullException(nameof(billRepository));
            _clientService = clientService;
            _billRepository = billRepository;
        }
        //тестить
        public List<ClientPayedBillsSum> GetPayedBillsSum(int count, string startDateTime, string endDateTime)
        {
            List<ClientPayedBillsSum> billsSumList = new List<ClientPayedBillsSum>();
            List<Bill> bills=new List<Bill>();

            if (startDateTime != "" && endDateTime != "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill.CreatedAt.CompareTo(startDateTime) <= 0 && bill.CreatedAt.CompareTo(endDateTime) >= 0
                    )
                    .ToList();
            }
            else if (startDateTime == "" && endDateTime != "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                           bill.CreatedAt.CompareTo(endDateTime) >= 0
                    )
                    .ToList();
            }
            else if (startDateTime != "" && endDateTime == "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill.CreatedAt.CompareTo(startDateTime) <= 0
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
                }
            }
            return billsSumList;
        }
        //тестить
        public ClientBillsStat GetClientBillsStat(int id, string startDateTime, string endDateTime)
        {
            //throw new NotImplementedException();
            List<Bill> bills = new List<Bill>();
            if (startDateTime != "" && endDateTime != "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill.CreatedAt.CompareTo(startDateTime) <= 0 && bill.CreatedAt.CompareTo(endDateTime) >= 0 && bill.ClientId==id
                    )
                    .ToList();
            }
            else if (startDateTime == "" && endDateTime != "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                           bill.CreatedAt.CompareTo(endDateTime) >= 0 && bill.ClientId == id
                    )
                    .ToList();
            }
            else if (startDateTime != "" && endDateTime == "")
            {
                bills = _billRepository
                    .All()
                    .Where(
                        bill =>
                            bill.CreatedAt.CompareTo(startDateTime) <= 0 && bill.ClientId == id
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
            ClientBillsStat clientBillsStat=new ClientBillsStat();
            clientBillsStat.TotalCount = bills.Count;
            clientBillsStat.PayedCount = bills.Count(bill => bill.WasPayed);
            clientBillsStat.UnpayedCount = clientBillsStat.TotalCount - clientBillsStat.PayedCount;
            clientBillsStat.TotalSum = bills.Sum(bill => bill.Sum);
            clientBillsStat.PayedSum = bills.Where(bill => bill.WasPayed).Sum(bill => bill.Sum);
            clientBillsStat.UnpayedSum = clientBillsStat.TotalSum - clientBillsStat.PayedSum;
            return clientBillsStat;
        }
        //тестить
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
