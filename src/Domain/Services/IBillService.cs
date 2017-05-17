using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
    public interface IBillService
    {
        Bill AddBill(int id,int number, decimal sum, int clientId, DateTime createdAt);
        bool PayBill(int id,DateTime createdAt);
        List<Bill> GetBills(int offset, int count);
        List<Bill> GetClientBills(int id, int offset, int count);
        DateTime StringToDateTime(string date);
    }
}
