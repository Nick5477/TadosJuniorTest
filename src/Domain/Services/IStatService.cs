using System.Collections.Generic;
using Domain.Structures;

namespace Domain.Services
{
    public interface IStatService
    {
        List<ClientPayedBillsSum> GetPayedBillsSum(int count, string startDateTime, string endDateTime);
        BillsStat GetClientBillsStat(int id, string startDateTime, string endDateTime);
        BillsStat GetAllBillsStat();
    }
}
