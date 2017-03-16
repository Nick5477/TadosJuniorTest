using Domain.Structures;

namespace Domain.Services
{
    public interface IStatService
    {
        ClientPayedBillsSum GetPayedBillsSum(int count, string StartDateTime, string EndDateTime);
        ClientBillsStat GetClientBillsStat(int id, string StartDateTime, string EndDateTime);
        BillsStat GetAllBillsStat();
    }
}
