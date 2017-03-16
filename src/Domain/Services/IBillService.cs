using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
    public interface IBillService
    {
        Bill AddBill(decimal sum, int clientId);
        void PayBill(int id,DateTime createdAt);
        List<Bill> GetBills(int offset, int count);
        int GetNewBillId();
        int GetNewBillNumber(int month);
    }
}
